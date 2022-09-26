using Common.HelperContract;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class RabbitMQHelper : IRabbitMQHelper
    {
        public bool EnqueueProductMailAnnouncement(string Message)
        {
            var RabbitMQFactory = new ConnectionFactory{
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "flowardVH",
                ClientProvidedName = "RabbitMQHelper"
            };
            using (var _connection = RabbitMQFactory.CreateConnection()){
                using (var _channel = _connection.CreateModel()){
                    _channel.QueueDeclare(
                        queue: "ProductAnnouncements",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var body = Encoding.UTF8.GetBytes(Message);

                    _channel.BasicPublish(
                        exchange: "ProductAnnouncementExchange",
                        routingKey: "floward",
                        basicProperties: null,
                        body: body);
                }
            }
            return true;
        }
    }
}
