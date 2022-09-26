using Common.HelperContract;
using Common.Models;
using Newtonsoft.Json;
using QueueService.BusinessContract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.InteropServices;
using System.Text;

namespace QueueService
{
    public class Worker : BackgroundService
    {
        #region Declarations
        private readonly ILogger<Worker> _logger;
        private readonly IMailMessenger _mailMessenger;
        private readonly IProductsNotification _productNotifications;
        private string _messageTemplate = "<HTML>\r\n\t<head></head>\r\n\t<body>\r\n\t\t<H1>New Product Was Added</H1>\r\n\t\t<H3>Name : [nameValue]</H3>\r\n\t\t<H3>Price : [priceValue] AED</H3>\r\n\t\t<H3>Cost : [costValue] AED</H3>\r\n\t\t<img src=\"[imgSource]\"/>\r\n\t</body>\r\n</HTML>";
        #endregion

        #region Constructor
        public Worker(ILogger<Worker> logger, IMailMessenger mailMessenger, IProductsNotification notification)
        {
            _logger = logger;
            _mailMessenger = mailMessenger;
            _productNotifications = notification;
        }
        #endregion

        #region Implementation
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var RabbitMQFactory = new ConnectionFactory
                    {
                        HostName = "localhost",
                        UserName = "guest",
                        Password = "guest",
                        VirtualHost = "flowardVH",
                        ClientProvidedName = "EmailService"
                    };
                    using (var _connection = RabbitMQFactory.CreateConnection())
                    using (var _channel = _connection.CreateModel())
                    {
                        _channel.QueueDeclare(
                            queue: "ProductAnnouncements",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
                        var _consumer = new EventingBasicConsumer(_channel);
                        _consumer.Received += async (model, e) =>
                        {
                            _logger.LogInformation(">>>>>>>>>>>>>>>>> Message Intercepted !! at {time}", DateTimeOffset.Now);
                            var body = e.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            #region To Be Removed
                            var _product = await _productNotifications.GetProduct(long.Parse(message));
                            _messageTemplate = _messageTemplate.Replace("[nameValue]", _product.Name);
                            _messageTemplate = _messageTemplate.Replace("[priceValue]", _product.Price.ToString());
                            _messageTemplate = _messageTemplate.Replace("[costValue]", _product.Cost.ToString());
                            _messageTemplate = _messageTemplate.Replace("[imgSource]", _product.Base64Image); 
                            #endregion

                            _mailMessenger.SendMail("m-assaf@outlook.com", "New Product", _messageTemplate);
                        };
                        _channel.BasicConsume(
                            queue: "ProductAnnouncements",
                            autoAck: true,
                            consumer: _consumer);
                    }
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
        } 
        #endregion
    }
}