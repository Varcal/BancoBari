using System;
using BancoBari.Domain.Entities;

namespace BancoBari.MessageBroker.Receive.v1
{
    class HelloWorldMessageReceive
    {
        public Guid ServiceId { get; set; }
        public HelloWorld Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid RequestId { get; set; }
    }
}
