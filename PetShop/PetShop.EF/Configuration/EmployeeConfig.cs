using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.Configuration {
    public class EmployeeConfig: IEntityTypeConfiguration<Employee> {

        public void Configure(EntityTypeBuilder<Employee> builder) {
        builder.ToTable("Employees");
        builder.HasKey(employee => employee.ID);
        builder.Property(employee => employee.ID).ValueGeneratedOnAdd();
        builder.Property(employee => employee.Name).HasMaxLength(maxLength: 100);
        builder.Property(employee => employee.Surname).HasMaxLength(maxLength: 100);
        builder.Property(employee => employee.SallaryPerMonth).HasColumnType("decimal(10, 2)");
        //builder.Property(employee => employee.SallaryPerMonth).HasColumnType("decimal(10, 2)");
        }


    }
}
