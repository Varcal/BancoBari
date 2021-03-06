﻿using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BancoBari.Domain.Entities;
using BancoBari.Infra.CrossCutting;
using BancoBari.Services.v1.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BancoBari.MessageBorker.Receive.v1
{
    public class MessageConsumer: BackgroundService
    {
        private MessageSettings _settings;
        private string _queueName;
        private IConnection _connection;
        private IModel _channel;
        private readonly ILogger _logger;
        private readonly ICustomerLogService _customerLogService;

        public MessageConsumer(IOptions<MessageSettings> options, IConfiguration configuration, ILogger<MessageConsumer> logger, ICustomerLogService customerLogService)
        {
            _logger = logger;
            _settings = options.Value;
            _queueName = configuration.GetSection("Queues:Consumer").Value;
            _customerLogService = customerLogService;
            InitializerQueueListener();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(string.IsNullOrWhiteSpace(_queueName)) return Task.CompletedTask;

            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var customerMessage = JsonSerializer.Deserialize<CustomerMessageReceive>(content);

                _logger.Log(LogLevel.Information, $"Mensagem recebida com ServiceId: {customerMessage.ServiceId} - {DateTime.Now}, {JsonSerializer.Serialize(customerMessage)}");
                HandleMessage(customerMessage.Content);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }


        private void InitializerQueueListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }


        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void HandleMessage(Customer customer)
        {
            _customerLogService.Logger(customer);
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }


        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
