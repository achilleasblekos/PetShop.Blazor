using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.Configuration {
    public class PetFoodConfig : IEntityTypeConfiguration<PetFood> {

        public void Configure(EntityTypeBuilder<PetFood> builder) {
            builder.ToTable("PetFood");
            builder.HasKey(petFood => petFood.ID);
            builder.Property(petFood => petFood.ID).ValueGeneratedOnAdd();
  
            builder.Property(petFood => petFood.Price).HasColumnType("decimal(10, 2)");
            builder.Property(petFood => petFood.Cost).HasColumnType("decimal(10, 2)");

        }

    }
}
