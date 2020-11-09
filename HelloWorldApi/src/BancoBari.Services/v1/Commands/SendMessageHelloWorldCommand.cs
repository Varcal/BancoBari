using BancoBari.Domain.Entities;
using MediatR;

namespace BancoBari.Services.v1.Commands
{
    public class SendMessageHelloWorldCommand: IRequest<HelloWorld>
    {
        public HelloWorld HelloWorld { get; set; }

        public SendMessageHelloWorldCommand()
        {
            HelloWorld = new HelloWorld();
        }
    }
}
