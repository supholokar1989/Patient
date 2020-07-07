using Microsoft.Azure.Cosmos;
using Patient.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Infrastructure.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        Container CurrentContainer { get; }
        Container HistoryContainer { get; }
        Task<bool> AppointmentReschedulingAsync(PatientVisitModel PatientVisit);
        Task<PatientVisitModel> GetPatientVisitByIdAsync(string id, string tenantId);
        Task<dynamic> GetPatientVisitByPatientId_AppointmentIdAsync(string patientId, string appointmentId, string tenantId);
        Task<bool> AddPatientVisitAsync(PatientVisitModel patientVisit, Container container);
        Task<bool> UpdatePatientVisitAsync(PatientVisitModel patientVisit, string ETag);
    }
}
