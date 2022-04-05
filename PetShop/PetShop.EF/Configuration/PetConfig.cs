using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.Configuration {
    public class PetConfig :IEntityTypeConfiguration<Pet> {

        public void Configure(EntityTypeBuilder<Pet> builder) {
            builder.ToTable("Pets");
            builder.HasKey(pet => pet.ID);
            builder.Property(pet => pet.ID).ValueGeneratedOnAdd();
            builder.Property(pet => pet.Breed).HasMaxLength(50);
            builder.Property(pet => pet.Price).HasColumnType("decimal(10, 2)");
            builder.Property(pet => pet.Cost).HasColumnType("decimal(10, 2)");

        }

    
    }
    
}
