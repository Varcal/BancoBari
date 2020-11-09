using BancoBari.Domain.Entities;
using MediatR;

namespace BancoBari.Services.v1
{
    public class CreateCustomerCommand: IRequest<Customer>
    {
        public Customer Customer { get; set; }


        public CreateCustomerCommand(Customer customer)
        {
            Customer = customer;
        }
    }
}
