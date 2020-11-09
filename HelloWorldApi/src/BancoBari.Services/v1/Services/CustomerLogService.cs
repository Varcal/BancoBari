using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using BancoBari.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BancoBari.Services.v1.Services
{
    public class CustomerLogService: ICustomerLogService
    {
        private readonly ILogger _logger;

        public CustomerLogService(ILogger<CustomerLogService> logger)
        {
            _logger = logger;
        }
        public void Logger(Customer customer)
        {
           _logger.Log(LogLevel.Information, $"Logado {JsonSerializer.Serialize(customer)}");
        }
    }
}
