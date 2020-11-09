using System;
using System.Threading;
using System.Threading.Tasks;
using BancoBari.Domain.Entities;
using BancoBari.MessageBroker.Send.v1;
using BancoBari.MessageBroker.Send.v1.Messages;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Timers = System.Timers;

namespace BancoBari.Services.v1.Commands
{
    public class HelloWorldCommandHandler : IRequestHandler<SendMessageHelloWorldCommand, HelloWorld>
    {
        private readonly IMessageProducer _messageProducer;
        private readonly string _queueName;
        private readonly Guid _serviceId;
        private readonly ILogger _logger;

        public HelloWorldCommandHandler(IConfiguration configuration,IMessageProducer messageProducer, ILogger<HelloWorldCommandHandler> logger)
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _serviceId = new Guid(configuration.GetSection("ServiceId").Value); 
            _queueName = configuration.GetSection("Queues:Producer").Value;
        }


        public async Task<HelloWorld> Handle(SendMessageHelloWorldCommand request, CancellationToken cancellationToken)
        {
            var timer = new Timers.Timer(5000)
            {
                AutoReset = true,
                Enabled = true,
            };

            timer.Elapsed += (sender, e) => TimeElapsed(sender, e, new HelloWorldMessage(_serviceId, request.HelloWorld));
            

            return await Task.FromResult(new HelloWorld());
        }

        private void TimeElapsed(object sender, Timers.ElapsedEventArgs e, HelloWorldMessage helloWorldMessage)
        {
            
            _messageProducer.SendMessage(_queueName, helloWorldMessage);
            _logger.Log(LogLevel.Information, $"{DateTime.Now} - Enviada mensagem pelo ServiceId: {helloWorldMessage.ServiceId} com RequestId: {helloWorldMessage.RequestId}");
        }
    }
}
