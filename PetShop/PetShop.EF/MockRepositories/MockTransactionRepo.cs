using PetShop.EF.Repositories;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.MockRepositories
{
    public class MockTransactionRepo : IEntityRepo<Transaction>
    {
        private List<Transaction> _transactions = new List<Transaction>
        {
            new Transaction(){ID = 1, CustomerID = 1,EmployeeID=1,PetID=1,PetPrice=300,PetFoodID=1,PetFoodQty=3,PetFoodPrice=15,TotalPrice=330},
            new Transaction(){ID = 2, CustomerID = 2,EmployeeID=1,PetID=2,PetPrice=50,PetFoodID=3,PetFoodQty=5,PetFoodPrice=5,TotalPrice=70},
        };

        public Task AddAsync(Transaction entity) {
            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id) {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Transaction>> GetAllAsync() {
            return Task.FromResult(_transactions.AsEnumerable());
        }

        public Task<Transaction?> GetByIdAsync(int id) {
            return Task.FromResult(_transactions.SingleOrDefault(transaction => transaction.ID == id));
        }

        public Task UpdateAsync(int id, Transaction entity) {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }

        private void AddLogic(Transaction entity)
        {
            if (entity.ID !=0)
                throw new ArgumentException("Given entity should not have ID set",nameof(entity));
            var lastId = _transactions.OrderBy(transaction => transaction.ID).Last().ID;
            entity.ID = ++lastId;
            _transactions.Add(entity);
        }

        private void DeleteLogic(int id)
        {
            var foundTransaction = _transactions.SingleOrDefault(transaction => transaction.ID == id);
            if (foundTransaction != null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _transactions.Remove(foundTransaction);
        }

        private void UpdateLogic(int id, Transaction entity)
        {
            var foundTransaction = _transactions.SingleOrDefault(transaction => transaction.ID==id);
            if (foundTransaction != null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            foundTransaction.Date = entity.Date;
            foundTransaction.CustomerID = entity.CustomerID;
            foundTransaction.EmployeeID = entity.EmployeeID;
            foundTransaction.PetID = entity.PetID;
            foundTransaction.PetPrice = entity.PetPrice;
            foundTransaction.PetFoodID = entity.PetFoodID;
            foundTransaction.PetFoodQty = entity.PetFoodQty;
            foundTransaction.PetFoodPrice = entity.PetFoodPrice;
            foundTransaction.TotalPrice = entity.TotalPrice;
        }
    }
}
