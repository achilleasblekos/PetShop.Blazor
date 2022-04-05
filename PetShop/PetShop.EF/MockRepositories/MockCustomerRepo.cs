using PetShop.EF.Context;
using PetShop.EF.Repositories;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.MockRepositories
{
    public class MockCustomerRepo : IEntityRepo<Customer>
    {
        private List<Customer> _customers = new List<Customer>
        {
            new Customer(){ ID=1, Name="Giannis",Surname="Polychroniadis", Phone = "1234567890", TIN="1234567890"},
            new Customer(){ ID=2, Name="Giorge",Surname="Aivaliotis", Phone="0987654321", TIN="0987654321"},
            new Customer(){ ID=3, Name="Theodoros",Surname="Petsagkas",Phone="1230987654", TIN="1230987654"},
            new Customer(){ID=4, Name="Dimitris",Surname="Tserkezidis",Phone="0981234567", TIN="0981234567"}
        };

        public Task AddAsync(Customer entity) {
            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id) {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Customer>> GetAllAsync() {
            return Task.FromResult(_customers.AsEnumerable());
        }

        public Task<Customer?> GetByIdAsync(int id) {
            return Task.FromResult(_customers.SingleOrDefault(customer => customer.ID == id));
        }

        public Task UpdateAsync(int id, Customer entity) {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }



        private void AddLogic(Customer entity) {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            var lastId = _customers.OrderBy(customer=>customer.ID).Last().ID;
            entity.ID = ++lastId;
            _customers.Add(entity);
        }

        private void DeleteLogic(int id) {
            var foundCustomer = _customers.SingleOrDefault(customer => customer.ID == id);
            if (foundCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found");

            _customers.Remove(foundCustomer);
        }


        private void UpdateLogic(int id, Customer entity) {
            var foundCustomer = _customers.SingleOrDefault(todo => todo.ID == id);
            if (foundCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found");
            foundCustomer.Name = entity.Name;
            foundCustomer.Surname = entity.Surname;
            foundCustomer.TIN = entity.TIN;
            foundCustomer.Phone = entity.Phone;
        }





    }
}


