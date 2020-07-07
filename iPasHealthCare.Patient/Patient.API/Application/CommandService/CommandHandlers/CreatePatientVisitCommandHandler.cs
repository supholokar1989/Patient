using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Patient.API.Application.CommandService.Command;
using Patient.Domain.Models;
using Patient.Infrastructure.Grpc.Interfaces;
using Patient.Infrastructure.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Patient.API.Application.CommandService.CommandHandlers
{
    public class CreatePatientVisitCommandHandler : IRequestHandler<CreatePatientVisitCommand, bool>
    {
        
        private Container _currentContainer;
        private Container _historyContainer;
        private readonly IDocumentRepository _documentRepository;
        private readonly IClientGRPCClientService _gRPCClientService;
        private readonly ILogger<CreatePatientVisitCommandHandler> _logger;

        public CreatePatientVisitCommandHandler(IDocumentRepository documentRepository, IClientGRPCClientService gRPCClientService, ILogger<CreatePatientVisitCommandHandler> logger)
        {
            _documentRepository = documentRepository;
            _gRPCClientService = gRPCClientService;
            _currentContainer = _documentRepository.CurrentContainer;
            _historyContainer = _documentRepository.HistoryContainer;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(CreatePatientVisitCommand request, CancellationToken cancellationToken)
        {
            bool saved = false;
                try
            {
                // Retrieve the facilityId from the tenant service by tenant id and facility code
                var facilityId = await _gRPCClientService.ClientFacilitySubscribesToModule(request.PatientVisit.TenantId, request.PatientVisit.PatientVisit.FacilityCode);

                // Verify if record exist in the patientvisit current document by tenant, facility, patient id and appointment id, The result will be dynamic, if no result found will return null
                var currentPatientVisit = await _documentRepository.GetPatientVisitByPatientId_AppointmentIdAsync(request.PatientVisit.PatientVisit.PatientId, request.PatientVisit.PatientVisit.AppointmentId, request.PatientVisit.TenantId); // facilityid
                if (currentPatientVisit == null)
                {
                    // Record is not exists, Add document with PatientVisitDocVersion - 1.0
                    request.PatientVisit.id = Guid.NewGuid().ToString();
                    _logger.LogInformation("Adding new document into Current Container with Id {Id} for Client {TenantId}, Patient Id {PatientId} and Patient AppoinmentId {AppointmentId} ", 
                        request.PatientVisit.id, request.PatientVisit.TenantId, request.PatientVisit.PatientVisit.PatientId, request.PatientVisit.PatientVisit.AppointmentId);
                    request.PatientVisit.PatientVisitDocVersion = 1;
                    saved = await _documentRepository.AddPatientVisitAsync(request.PatientVisit, _currentContainer);                        
                }
                else
                {
                    //Convert Dynamic value into EtagModel as reading the ETag directly is not working 
                    var etagDoc = currentPatientVisit.ToObject<ETagCosmosDocument>();
                    //Convert Dynamic value into PatientVisitModel
                    PatientVisitModel CurrentVistdocument = currentPatientVisit.ToObject<PatientVisitModel>();
                    // Update PatientVisit Document Version number and replace all required field basis on Event Type and replay the exisitng with the new file
                    request.PatientVisit.id = CurrentVistdocument.id;
                    //Merge Part of Esiting Visit Document into newly request document according to the current Event type 

                    //Update the current document version before adding into the Current Container
                    request.PatientVisit.PatientVisitDocVersion = CurrentVistdocument.PatientVisitDocVersion + 1;
                    _logger.LogInformation("Adding Updated document into Current Container with existing Id {Id} for Client {TenantId}, Patient Id {PatientId} and Patient AppoinmentId {AppointmentId} ",
                        request.PatientVisit.id, request.PatientVisit.TenantId, request.PatientVisit.PatientVisit.PatientId, request.PatientVisit.PatientVisit.AppointmentId);
                    //Replace the existing Document with new Document
                    saved = await _documentRepository.UpdatePatientVisitAsync(request.PatientVisit, etagDoc._etag);
                    //Add document in a history Container, before that assign new ID
                    CurrentVistdocument.id = Guid.NewGuid().ToString();
                    _logger.LogInformation("Adding new document into History Container with Id {Id} for Client {TenantId}, Patient Id {PatientId} and Patient AppoinmentId {AppointmentId} ",
                        CurrentVistdocument.id, CurrentVistdocument.TenantId, CurrentVistdocument.PatientVisit.PatientId, CurrentVistdocument.PatientVisit.AppointmentId);
                    saved = await _documentRepository.AddPatientVisitAsync(CurrentVistdocument, _historyContainer);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return saved;            
        }
    }
}
