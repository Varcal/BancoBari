using System;
using BancoBari.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BancoBari.Domain.Intefaces;
using BancoBari.MessageBroker.Send.v1;
using BancoBari.MessageBroker.Send.v1.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BancoBari.Services.v1
{
    public class CustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private IMessageProducer _messageProducer;
        private readonly string _queueName;
        private readonly ICustomerRepository _customerRepository;
        private readonly Guid _serviceId;
        private readonly ILogger _logger;

        public CustomerCommandHandler(IConfiguration configuration, IMessageProducer messageProducer, ICustomerRepository customerRepository, ILogger<CustomerCommandHandler> logger)
        {
            _messageProducer = messageProducer;
            _customerRepository = customerRepository;
            _logger = logger;
            _queueName = configuration.GetSection("Queues:Producer").Value;
            _serviceId = new Guid(configuration.GetSection("ServiceId").Value);
        }


        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            
            var customer = await _customerRepository.AddAsync(request.Customer);
            var customerMessage = new CustomerMessage(_serviceId, customer);
            _messageProducer.SendMessage(_queueName, customerMessage);
            _logger.Log(LogLevel.Information, $"{DateTime.Now} - Enviada mensagem pelo ServiceId: {customerMessage.ServiceId} com RequestId: {customerMessage.RequestId}");
            return await Task.FromResult(customer);
        }
    }
}
