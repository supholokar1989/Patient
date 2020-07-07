using iPas.Infrastructure.EventBus.Events;
using Patient.Domain.Models;

namespace Patient.Domain.Events
{
    public class RegistrationReceivedIntegrationEvent : IntegrationEvent
    {
        public PatientVisitModel PatientVisit { get; }

        public RegistrationReceivedIntegrationEvent(PatientVisitModel patientVisit)
        {
            PatientVisit = patientVisit;
        }

        public RegistrationReceivedIntegrationEvent()
        {

        }
    }
}
