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
    public class TransactionRepo : IEntityRepo<Transaction>
    {
        private readonly PetShopContext _context;

        public TransactionRepo(PetShopContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(Transaction entity) {
            AddLogic(entity,_context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            DeleteLogic(id, _context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync() {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id) {
            return await _context.Transactions.SingleOrDefaultAsync(transaction => transaction.ID == id);
        }

        public async Task UpdateAsync(int id, Transaction entity) {
            UpdateLogic(id, entity,_context);
            await _context.SaveChangesAsync();
        }

        private void AddLogic(Transaction entity,PetShopContext context)
        {
            if (entity.ID !=0)
                throw new ArgumentException("Given entity should not have ID set", nameof(entity));
            context.Transactions.Add(entity);
        }
        private void DeleteLogic(int id,PetShopContext context)
        {
            var dbTransaction = context.Transactions.SingleOrDefault(transaction => transaction.ID == id);
            if (dbTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            context.Transactions.Remove(dbTransaction);
        }
        private void UpdateLogic(int id, Transaction entity, PetShopContext context)
        {
            var dbTransaction = context.Transactions.SingleOrDefault(transaction => transaction.ID == id);
            if (dbTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbTransaction.Date=entity.Date;
            dbTransaction.CustomerID = entity.CustomerID;
            dbTransaction.EmployeeID = entity.EmployeeID;
            dbTransaction.PetID=entity.PetID;
            dbTransaction.PetPrice=entity.PetPrice;
            dbTransaction.PetFoodID=entity.PetFoodID;
            dbTransaction.PetFoodQty=entity.PetFoodQty;
            dbTransaction.PetFoodPrice=entity.PetFoodPrice;
            dbTransaction.TotalPrice=entity.TotalPrice;

        }
    }
}
