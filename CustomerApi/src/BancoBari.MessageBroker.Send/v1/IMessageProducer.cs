using BancoBari.MessageBroker.Send.v1.Messages;

namespace BancoBari.MessageBroker.Send.v1
{
    public interface IMessageProducer
    {
        void SendMessage<T>(string queueName, Message<T> message) where T: class;
    }
}