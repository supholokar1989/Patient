using RabbitMQ.Client;
using System;

namespace iPas.Infrastructure.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
       : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
