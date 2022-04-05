using Microsoft.EntityFrameworkCore;
using PetShop.EF.Configuration;
using PetShop.Model;

namespace PetShop.EF.Context
{
    public class PetShopContext : DbContext {


        public PetShopContext() {

        }
   
        //New ctor ????
        public PetShopContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetFood> PetFoods { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new PetConfig());
            modelBuilder.ApplyConfiguration(new PetFoodConfig());
            modelBuilder.ApplyConfiguration(new TransactionConfig());

            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            //var connString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog = PetShopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connString = @"Data Source=LAPTOP-97BN5M6S\GEORGE;Initial Catalog=YellowPetshopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connString);
        }




    }
}
