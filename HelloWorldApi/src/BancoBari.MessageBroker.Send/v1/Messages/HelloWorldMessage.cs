using BancoBari.Domain.Entities;
using System;

namespace BancoBari.MessageBroker.Send.v1.Messages
{
    public class HelloWorldMessage: Message<HelloWorld>
    {
        public HelloWorldMessage(Guid serviceId, HelloWorld content) : base(serviceId, content)
        {
        }
    }
}
