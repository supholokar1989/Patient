using iPas.Infrastructure.EventBus.Abstractions;
using iPas.Infrastructure.EventBus.Events;
using Patient.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Infrastructure.Repository
{
    public class RegistrationIntegrationEventService : IRegistrationIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        public RegistrationIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            throw new NotImplementedException();
        }

        public Task PublishEventsThroughEventBusAsync(Guid transactionI)
        {
            throw new NotImplementedException();
        }
    }
}
