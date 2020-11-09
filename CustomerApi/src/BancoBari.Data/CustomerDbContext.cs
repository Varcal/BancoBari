using BancoBari.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BancoBari.Data
{
    public sealed class CustomerDbContext: DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Customer> Customers { get; set; }


        public CustomerDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
