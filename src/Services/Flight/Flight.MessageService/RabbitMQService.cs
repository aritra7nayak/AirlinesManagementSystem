using Flight.DTOs;
using Flight.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;

namespace Flight.MessageService
{
    public class RabbitMQService
    {
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMQService(IOptions<RabbitMQOptions> rabbitMQOptions)
        {
            _rabbitMQOptions = rabbitMQOptions.Value;

            // Create a connection factory with options
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQOptions.HostName,
                Port = _rabbitMQOptions.Port,
                UserName = _rabbitMQOptions.UserName,
                Password = _rabbitMQOptions.Password,
                VirtualHost = "/",
                //   Uri = new Uri("amqp://guest:guest@localhost:5672/")
            };

           

            // Establish a connection
            _connection = factory.CreateConnection();

            // Create a channel
            _channel = _connection.CreateModel();
        }

        public void PublishFlightData(IEnumerable<PublishAeroplane> flightData)
        {
            // Serialize flight data to JSON
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(flightData);

            // Convert the message to bytes
            var body = Encoding.UTF8.GetBytes(message);

            // Publish the message to the specified RabbitMQ exchange and routing key
            _channel.BasicPublish(exchange: "", routingKey: "flight_data_queue", basicProperties: null, body: body);

            Console.WriteLine($"Sent flight data: {message}");
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }


}
