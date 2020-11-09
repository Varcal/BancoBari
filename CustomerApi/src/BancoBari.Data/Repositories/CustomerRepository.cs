using System;

using System.Threading.Tasks;
using BancoBari.Domain.Entities;
using BancoBari.Domain.Intefaces;

namespace BancoBari.Data
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerRepository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
