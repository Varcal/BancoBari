using System.Threading.Tasks;
using BancoBari.Domain.Entities;

namespace BancoBari.Domain.Intefaces
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer requestCustomer);
    }
}
