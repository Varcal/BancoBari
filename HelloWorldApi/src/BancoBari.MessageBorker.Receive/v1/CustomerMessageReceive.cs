using System;
using BancoBari.Domain.Entities;

namespace BancoBari.MessageBorker.Receive.v1
{
    class CustomerMessageReceive
    {
        public Guid ServiceId { get; set; }
        public Customer Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid RequestId { get; set; }
    }
}
