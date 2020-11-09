using System;
using BancoBari.Domain.Entities;

namespace BancoBari.MessageBroker.Send.v1.Messages
{
    public class CustomerMessage: Message<Customer>
    {
        public CustomerMessage(Guid serviceId, Customer content) : base(serviceId, content)
        {
        }

        public CustomerMessage():base()
        {

        }
       
    }
}
