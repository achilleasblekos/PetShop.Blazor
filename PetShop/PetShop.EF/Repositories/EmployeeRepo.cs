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
    public class EmployeeRepo : IEntityRepo<Employee>
    {
        private readonly PetShopContext context;


        public EmployeeRepo(PetShopContext  _context) {
            context = _context;
        }
        public async Task AddAsync(Employee entity) {
           

            AddLogic(entity, context);
            await  context.SaveChangesAsync(); 
        }

        public async Task Create(Employee entity)
        {
            using var context = new PetShopContext();
            context.Employees.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            using var context = new PetShopContext();
            DeleteLogic(context, id);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            DeleteLogic(context, id);
           await  context.SaveChangesAsync(); 
        }

        public  List<Employee> GetAll()
        {
   
            return context.Employees.ToList();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() {
 
            return await context.Employees.ToListAsync();
        }

        public Employee? GetById(int id)
        {         
            using var context = new PetShopContext();
            //return await context.Employees.AsNoTracking().Include(employee=>employee.Transactions).SingleOrDefaultAsync(employee=>employee.ID==id);
            return context.Employees.Where(employee => employee.ID == id).SingleOrDefault();
        }

        public async Task<Employee?> GetByIdAsync(int id) {

            //return await context.Employees.AsNoTracking().Include(employee=>employee.Transactions).SingleOrDefaultAsync(employee=>employee.ID==id);
            return await context.Employees.Where(employee => employee.ID == id).SingleOrDefaultAsync();
        }

        public async Task Update(int id, Employee entity)
        {
            using var context = new PetShopContext();
            UpdateLogic(entity, context, id);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Employee entity) {
            UpdateLogic(entity, context, id);
            await context.SaveChangesAsync(); ;
        }

        public void AddLogic(Employee entity, PetShopContext context) {
            if(entity.ID != 0) {
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            }
            context.Employees.Add(entity);
        }

        private void UpdateLogic(Employee entity, PetShopContext context, int id) {
            var foundEmployee = context.Employees.SingleOrDefault(employee => employee.ID == id);
            if (foundEmployee is null)
                return;

            foundEmployee.Name = entity.Name;
            foundEmployee.Surname = entity.Surname;
            foundEmployee.EmployeeType = entity.EmployeeType;
            foundEmployee.SallaryPerMonth = entity.SallaryPerMonth;
        }

        private void DeleteLogic(PetShopContext context, int id) {
            var foundEmployee = context.Employees.SingleOrDefault(employee => employee.ID == id);
            if (foundEmployee is null)
               throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Employees.Remove(foundEmployee);
            context.SaveChangesAsync();
        }
    }
}
