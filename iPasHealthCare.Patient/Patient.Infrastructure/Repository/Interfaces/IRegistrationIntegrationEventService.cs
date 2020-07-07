using iPas.Infrastructure.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Infrastructure.Repository.Interfaces
{
    public interface IRegistrationIntegrationEventService
    {
        Task AddAndSaveEventAsync(IntegrationEvent evt);
        Task PublishEventsThroughEventBusAsync(Guid transactionI);
    }
}
