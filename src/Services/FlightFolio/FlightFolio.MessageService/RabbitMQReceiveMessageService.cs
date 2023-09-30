﻿using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using FlightFolio.Models;
using FlightFolio.DTOs;

namespace FlightFolio.MessageService
{
    public class RabbitMQReceiveMessageService :IDisposable
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;


        public RabbitMQReceiveMessageService(IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;

            // Create a connection factory with options
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQOptions.HostName,
                Port = _rabbitMQOptions.Port,
                UserName = _rabbitMQOptions.UserName,
                Password = _rabbitMQOptions.Password
            };

            // Establish a connection
            _connection = factory.CreateConnection();

            // Create a channel
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "flight_data_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Create a consumer
            _consumer = new EventingBasicConsumer(_channel);

            // Register an event handler for when a message is received
            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the message to Flight object
                var flightData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PublishedAeroplane>>(message);

                // Process the flight data (e.g., save to a database or perform other actions)
                Console.WriteLine($"Received flight data: {message}");
            };
            StartListening();


        }

        public void StartListening()
        {
            _channel.BasicConsume(queue: "flight_data_queue", autoAck: true, consumer: _consumer);
        }

        public void StopListening()
        {
            // Stop message consumption gracefully if needed
        }

        // Dispose method to handle cleanup
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
