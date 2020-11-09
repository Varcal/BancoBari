using BancoBari.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoBari.Data.Mappings
{
    public class CustomerMap: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(200)")
                .IsRequired();
        }
    }
}
