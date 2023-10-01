using System;
using System.Threading;
using System.Threading.Tasks;
using FlightFolio.MessageService;
using FlightFolio.Repository;
using Microsoft.Extensions.Hosting;

public class RabbitMQConsumerHostedService : IHostedService
{
    private readonly RabbitMQReceiveMessageService _rabbitMQConsumerService;
    private readonly AeroplaneRepository _aeroplaneRepository;

    public RabbitMQConsumerHostedService(RabbitMQReceiveMessageService rabbitMQConsumerService, AeroplaneRepository aeroplaneRepository)
    {
        _rabbitMQConsumerService = rabbitMQConsumerService;
        _aeroplaneRepository = aeroplaneRepository;
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
