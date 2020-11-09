using System.Reflection;
using BancoBari.Data;
using BancoBari.Domain.Entities;
using BancoBari.Domain.Intefaces;
using BancoBari.MessageBroker.Receive.v1;
using BancoBari.MessageBroker.Send.v1;
using BancoBari.Services.v1;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BancoBari.IoC
{
    public class IocConfig
    {
        public static void RegisterServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<CreateCustomerCommand,Customer>, CustomerCommandHandler>();
            services.AddTransient<IMessageProducer, MessageProducer>();
            services.AddHostedService<MessageConsumer>();
            services.AddDbContext<CustomerDbContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }
    }
}
