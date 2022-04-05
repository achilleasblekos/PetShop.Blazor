using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.Configuration {
    public class CustomerConfig : IEntityTypeConfiguration<Customer> {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.ToTable("Customer", "PetShop");
            builder.HasKey(customer=>customer.ID);
            builder.Property(customer => customer.ID).ValueGeneratedOnAdd();
            builder.Property(customer => customer.Name).HasMaxLength(maxLength: 100);
            builder.Property(customer => customer.Surname).HasMaxLength(maxLength: 100);
            builder.Property(customer => customer.Phone).HasMaxLength(maxLength: 10);
            builder.Property(customer => customer.TIN).HasMaxLength(maxLength: 9);


        }
    }
}
