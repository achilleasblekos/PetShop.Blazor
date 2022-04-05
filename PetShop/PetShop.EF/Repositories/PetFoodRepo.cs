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
    public class PetFoodRepo : IEntityRepo<PetFood>
    {
        private readonly PetShopContext context;

        public PetFoodRepo(PetShopContext dbContext)
        {
            context = dbContext;
        }
        public async Task AddAsync(PetFood entity) {
            AddLogic(entity, context);
            await context.SaveChangesAsync();
        }                

        public async Task DeleteAsync(int id) {
            DeleteLogic(id, context);
            await context.SaveChangesAsync();
        }        

        public async Task<IEnumerable<PetFood>> GetAllAsync() {
            return await context.PetFoods.ToListAsync();
        }        

        public async Task<PetFood?> GetByIdAsync(int id) {
            return await context.PetFoods.SingleOrDefaultAsync(PetFood => PetFood.ID == id);
        }       

        public async Task UpdateAsync(int id, PetFood entity) {
            UpdateLogic(id, entity, context);
            await context.SaveChangesAsync();
        }

        private void AddLogic(PetFood entity, PetShopContext context)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.PetFoods.Add(entity);
        }


        private void DeleteLogic(int id, PetShopContext context)
        {
            var dbPetFood = context.PetFoods.SingleOrDefault(petFood => petFood.ID == id);
            if (dbPetFood is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.PetFoods.Remove(dbPetFood);
        }

        private void UpdateLogic(int id, PetFood entity, PetShopContext context)
        {
            var dbPetFood = context.PetFoods.SingleOrDefault(petFood => petFood.ID == id);
            if (dbPetFood is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");


            dbPetFood.AnimalType = entity.AnimalType;
            dbPetFood.Price = entity.Price;
            dbPetFood.Cost = entity.Cost;           
        }
    }
}
