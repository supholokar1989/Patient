using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iPas.Infrastructure.EventBus.Abstractions;

namespace iPas.Infrastructure.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
