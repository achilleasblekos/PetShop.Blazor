using PetShop.EF.Repositories;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.MockRepositories
{
    public class MockEmployeeRepo : IEntityRepo<Employee>
    {
        private List<Employee> _employees = new List<Employee>
        {
            new Employee(){ID = 1, Name ="Takis",Surname="Manageridis",EmployeeType=EmployeeType.Manager, SallaryPerMonth=2000m},
            new Employee(){ID = 2, Name ="Akis",Surname="Staffikis",EmployeeType=EmployeeType.Staff, SallaryPerMonth=450.60m},
            //new Employee(){ID = 2, Name ="Akis",Surname="Staffikis",EmployeeType=EmployeeType.Staff, SallaryPerMonth=450.60m},

        };

        public void Create(Employee entity)
        {
            _employees.Add(entity);
        }
        public List<Employee> GetAll() {
            return _employees;
        }
        public Task<IEnumerable<Employee>> GetAllAsync() { // 
            return Task.FromResult(_employees.AsEnumerable());
        }
        public Employee? GetById(int id) {
            return _employees.SingleOrDefault(employee => employee.ID == id);
        }
        public Task<Employee?> GetByIdAsync(int id) {
            return Task.FromResult(_employees.SingleOrDefault(employee => employee.ID == id));
        }

        public void Delete(int id)
        {
            var foundEmployee = _employees.SingleOrDefault(employee => employee.ID == id);
            if (foundEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _employees.Remove(foundEmployee);
        }

        public async Task DeleteAsync(int id) {
            DeleteLogic(id);
        }


        public void Update(int id, Employee entity)
        {
            UpdateLogic(id, entity);
        }

        public Task UpdateAsync(int id, Employee entity) {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }

        public Task AddAsync(Employee entity) {
            AddLogic(entity);
            return Task.CompletedTask;  
        }

        private void AddLogic(Employee entity) {
            if(entity.ID != 0) {
                throw new ArgumentException("Given entity should not have ID set", nameof(entity));
            }
            var lastId = _employees.OrderBy(employee => employee.ID).Last().ID;
            entity.ID = ++lastId;
            _employees.Add(entity);
        }
        private void UpdateLogic(int id, Employee entity) {
            var foundEmployee = _employees.SingleOrDefault(employee => employee.ID == id);
            if (foundEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            foundEmployee.Name = entity.Name;
            foundEmployee.Surname = entity.Surname;
            foundEmployee.EmployeeType = entity.EmployeeType;
            foundEmployee.SallaryPerMonth = entity.SallaryPerMonth;
        }
        private void DeleteLogic(int id) {
            var foundEmployee = _employees.SingleOrDefault(employee => employee.ID == id);
            if (foundEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _employees.Remove(foundEmployee);
        }
    }
}
