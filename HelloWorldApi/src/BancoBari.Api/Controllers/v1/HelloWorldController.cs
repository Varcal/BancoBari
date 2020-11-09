using System.Threading.Tasks;
using BancoBari.Services.v1;
using BancoBari.Services.v1.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BancoBari.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HelloWorldController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {

            await _mediator.Send(new SendMessageHelloWorldCommand());

            return Ok();
        }
    }
}
