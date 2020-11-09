using BancoBari.Domain.Entities;

namespace BancoBari.Services.v1.Services
{
    public interface ICustomerLogService
    {
        void Logger(Customer customer);
    }
}
