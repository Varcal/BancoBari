using System.Reflection;
using BancoBari.Domain.Entities;
using BancoBari.MessageBorker.Receive.v1;
using BancoBari.MessageBroker.Send.v1;
using BancoBari.Services.v1.Commands;
using BancoBari.Services.v1.Services;
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

            services.AddTransient<IRequestHandler<SendMessageHelloWorldCommand,HelloWorld>, HelloWorldCommandHandler>();

            services.AddTransient<IMessageProducer, MessageProducer>();

            services.AddHostedService<MessageConsumer>();

            services.AddTransient<ICustomerLogService, CustomerLogService>();
        }
    }
}
