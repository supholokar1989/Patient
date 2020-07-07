using FluentValidation.Results;
using MediatR;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Patient.API.Application.CommandService;
using Patient.API.Application.CommandService.CommandHandlers;
using Patient.Infrastructure.Grpc.Interfaces;
using Patient.Domain.Models;
using Patient.Infrastructure.Repository.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Patient.API.Application.CommandService.Command;

namespace Patient.Test
{
    [TestFixture]
    public class CreatePatientVisitCommandHandlerTests
    {

        private Mock<IMediator> _mediator;
        private Container _currentContainer;
        private Container _historyContainer;
        private Mock<IDocumentRepository> _documentRepository;
        private Mock<IClientGRPCClientService> _clientGRPCClientService;
        private CreatePatientVisitCommand _createPatientVisitCommand;
        private CreatePatientVisitCommandHandler _createPatientVisitCommandHandler;

        PatientVisitModel patientVisit_S12;
        PatientVisitModel patientVisit_S14;

        [SetUp]
        public void Setup()
        {
            string jsonFile_S12_NewPatientAppointment = "D:\\PELITAS\\PROJECTS\\IPAS_PatientService\\iPasHealthCare.Patient\\Patient.Test\\TestData\\S12_NewPatientAppointment.json";
            string jsonFile_S14_ExistingAppointmentUpdate = "D:\\PELITAS\\PROJECTS\\IPAS_PatientService\\iPasHealthCare.Patient\\Patient.Test\\TestData\\S14_ExistingAppointmentUpdate.json";
            _documentRepository = new Mock<IDocumentRepository>();
            _clientGRPCClientService = new Mock<IClientGRPCClientService>();
            _mediator = new Mock<IMediator>();

            var docRepoObj = _documentRepository.Object;
            _currentContainer = docRepoObj.CurrentContainer;
            _historyContainer = docRepoObj.HistoryContainer;
            patientVisit_S12 = ConvertJsonToModel(jsonFile_S12_NewPatientAppointment);
            patientVisit_S14 = ConvertJsonToModel(jsonFile_S14_ExistingAppointmentUpdate);

            _createPatientVisitCommandHandler = new CreatePatientVisitCommandHandler(_documentRepository.Object, _clientGRPCClientService.Object);
        }

        [Test]
        public async Task ShouldAddNewPatient_WhenCurrentContainerNotExistsThePatient()
        {
            _documentRepository
                .Setup(_ => _.GetPatientVisitByPatientId_AppointmentIdAsync(patientVisit_S12.PatientVisit.PatientId, patientVisit_S12.PatientVisit.AppointmentId, patientVisit_S12.TenantId))
                .Returns(Task.FromResult<PatientVisitModel>(null));
            _documentRepository
                .Setup(_ => _.AddPatientVisitAsync(patientVisit_S12, _currentContainer)).Returns(Task.FromResult<bool>(true));
            _createPatientVisitCommand = new CreatePatientVisitCommand(patientVisit_S12);
            var result = await _createPatientVisitCommandHandler.Handle(_createPatientVisitCommand, new System.Threading.CancellationToken());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ShouldUpdatePatient_WhenCurrentContainerExistsThePatient_CreateHistoryofExistedPatient()
        {
            _documentRepository
                .Setup(_ => _.GetPatientVisitByPatientId_AppointmentIdAsync(patientVisit_S12.PatientVisit.PatientId, patientVisit_S12.PatientVisit.AppointmentId, patientVisit_S12.TenantId))
                .Returns(Task.FromResult<PatientVisitModel>(patientVisit_S14));
            _documentRepository
                .Setup(_ => _.UpdatePatientVisitAsync(patientVisit_S12)).Returns(Task.FromResult<bool>(true));
            _createPatientVisitCommand = new CreatePatientVisitCommand(patientVisit_S12);
            _documentRepository
                .Setup(_ => _.AddPatientVisitAsync(patientVisit_S14, _historyContainer)).Returns(Task.FromResult<bool>(true));
            _createPatientVisitCommand = new CreatePatientVisitCommand(patientVisit_S12);
            var result = await _createPatientVisitCommandHandler.Handle(_createPatientVisitCommand, new System.Threading.CancellationToken());
            Assert.IsTrue(result);
        }

        [Ignore("Ignore test")]
        public PatientVisitModel ConvertJsonToModel(string json)
        {
            PatientVisitModel patientVisitModel;
            using (StreamReader r = new StreamReader(json))
            {
                string jsonResult = r.ReadToEnd();
                patientVisitModel = JsonConvert.DeserializeObject<PatientVisitModel>(jsonResult);
            }
            return patientVisitModel;
        }

        [TearDown]
        public void Cleanup()
        {
            _createPatientVisitCommand = null;
            _createPatientVisitCommandHandler = null;
        }
    }
}