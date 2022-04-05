using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.Repositories
{
    public class CustomerRepo : IEntityRepo<Customer> {

        private readonly PetShopContext context;



        public CustomerRepo(PetShopContext dbContext) {
            context=dbContext;
        }


        public async Task AddAsync(Customer entity) {

            AddLogic(entity, context);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            DeleteLogic(id, context);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id) {
            return await context.Customers.SingleOrDefaultAsync(customer => customer.ID == id);
        }

        public async Task UpdateAsync(int id, Customer entity) {
            UpdateLogic(id, entity, context);
            await context.SaveChangesAsync();
        }





        private void AddLogic(Customer entity, PetShopContext context) {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Customers.Add(entity);
        }


        private void DeleteLogic(int id, PetShopContext context) {
            var dbCustomer = context.Customers.SingleOrDefault(customer => customer.ID == id);
            if (dbCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Customers.Remove(dbCustomer);
        }

        private void UpdateLogic(int id, Customer entity, PetShopContext context) {
            var dbCustomer = context.Customers.SingleOrDefault(customer => customer.ID == id);
            if (dbCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbCustomer.Name = entity.Name;
            dbCustomer.Surname = entity.Surname;
            dbCustomer.TIN = entity.TIN;
            dbCustomer.Phone = entity.Phone;
          
        }
    }


}
