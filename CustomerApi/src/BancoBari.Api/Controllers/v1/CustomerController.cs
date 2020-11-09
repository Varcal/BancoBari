using System.Threading.Tasks;
using BancoBari.Api.Models;
using BancoBari.Domain.Entities;
using BancoBari.Services.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BancoBari.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterNewCustomer registerNewCustomer)
        {
            await _mediator.Send(new CreateCustomerCommand(new Customer(registerNewCustomer.Name)));

            return Ok();
        }
    }
}
