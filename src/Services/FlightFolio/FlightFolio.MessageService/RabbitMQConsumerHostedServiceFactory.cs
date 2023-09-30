using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.MessageService
{
    public class RabbitMQConsumerHostedServiceFactory : IDisposable
    {
        private readonly IServiceProvider _rootServiceProvider;
        private IServiceScope _scope;
        private RabbitMQConsumerHostedService _service;

        public RabbitMQConsumerHostedServiceFactory(IServiceProvider rootServiceProvider)
        {
            _rootServiceProvider = rootServiceProvider;
        }

        public RabbitMQConsumerHostedService Create()
        {
            if (_service == null)
            {
                _scope = _rootServiceProvider.CreateScope(); // Create a new scope
                var scopedServiceProvider = _scope.ServiceProvider;
                _service = ActivatorUtilities.CreateInstance<RabbitMQConsumerHostedService>(scopedServiceProvider);
            }
            return _service;
        }

        public void Dispose()
        {
            _scope?.Dispose(); // Dispose of the scope
        }
    }

}
