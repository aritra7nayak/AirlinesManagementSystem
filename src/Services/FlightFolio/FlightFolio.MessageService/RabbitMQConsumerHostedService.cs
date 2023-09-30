using System;
using System.Threading;
using System.Threading.Tasks;
using FlightFolio.MessageService;
using Microsoft.Extensions.Hosting;

public class RabbitMQConsumerHostedService : IHostedService
{
    private readonly RabbitMQReceiveMessageService _rabbitMQConsumerService;

    public RabbitMQConsumerHostedService(RabbitMQReceiveMessageService rabbitMQConsumerService)
    {
        _rabbitMQConsumerService = rabbitMQConsumerService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _rabbitMQConsumerService.StartListening();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitMQConsumerService.StopListening();
        return Task.CompletedTask;
    }
}
