using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetShop.EF.Configuration {
    public class TransactionConfig : IEntityTypeConfiguration<Transaction> {

        public void Configure(EntityTypeBuilder<Transaction> builder) {
            builder.ToTable("Transactions");
            builder.HasKey(transaction => transaction.ID);
            builder.Property(transaction => transaction.ID).ValueGeneratedOnAdd();

            builder.Property(transaction => transaction.PetPrice).HasColumnType("decimal(10,2)");
            builder.Property(transaction => transaction.PetFoodQty).HasColumnType("int");
            builder.Property(transaction => transaction.PetFoodPrice).HasColumnType("decimal(10,2)");
            builder.Property(transaction => transaction.TotalPrice).HasColumnType("decimal(10,2)");


            builder.HasOne(transaction => transaction.Customer).WithMany(customer => customer.Transactions).HasForeignKey(transaction => transaction.CustomerID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(transaction => transaction.Employee).WithMany(employee => employee.Transactions).HasForeignKey(transaction => transaction.EmployeeID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(transaction => transaction.Pet).WithOne(pet => pet.Transaction).HasForeignKey<Transaction>(transaction => transaction.PetID).OnDelete(DeleteBehavior.Restrict);
            


        }


    }
}
