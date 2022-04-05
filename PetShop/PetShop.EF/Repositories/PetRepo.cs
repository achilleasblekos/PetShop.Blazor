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
    public class PetRepo : IEntityRepo<Pet>
    {
        private readonly PetShopContext _context;

        public PetRepo(PetShopContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddAsync(Pet entity) {
            AddLogic(entity,_context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            DeleteLogic(id,_context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllAsync() {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id) {
            return await _context.Pets.SingleOrDefaultAsync(pet => pet.ID==id);
        }

        public async Task UpdateAsync(int id, Pet entity) {
            UpdateLogic(id, entity,_context);
            await _context.SaveChangesAsync();
        }

        private void AddLogic(Pet entity, PetShopContext context)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have ID set", nameof(entity));
            context.Pets.Add(entity);
        }

        private void DeleteLogic(int id, PetShopContext context)
        {
            var dbPet=context.Pets.SingleOrDefault(pet => pet.ID == id);
            if (dbPet is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            context.Pets.Remove(dbPet);
        }

        private void UpdateLogic(int id, Pet entity,PetShopContext context)
        {
            var dbPet = context.Pets.SingleOrDefault(pet => pet.ID == id);
            if (dbPet is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            dbPet.Breed=entity.Breed;
            dbPet.AnimalType=entity.AnimalType;
            dbPet.PetStatus = entity.PetStatus;
            dbPet.Price = entity.Price;
            dbPet.Cost = entity.Cost;
        }
    }
}
