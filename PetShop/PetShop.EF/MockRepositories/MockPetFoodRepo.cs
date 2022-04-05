using PetShop.EF.Repositories;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.MockRepositories
{
    public class MockPetFoodRepo : IEntityRepo<PetFood>
    {
        private readonly List<PetFood> _petFoods = new List<PetFood>
        {
            new PetFood(){ID=1,AnimalType=AnimalType.Mammal,Price=15,Cost=10},
            new PetFood(){ID=2,AnimalType=AnimalType.Reptile,Price=20,Cost=10},
            new PetFood(){ID=3,AnimalType =AnimalType.Bird,Price=5,Cost=1},
            new PetFood(){ID=4,AnimalType =AnimalType.Fish,Price=5,Cost=1}
        };

        public Task AddAsync(PetFood entity) {
            _petFoods.Add(entity);
            return Task.CompletedTask;
        }        

        public Task Delete(int id)
        {

            var foundFood = _petFoods.SingleOrDefault(food => food.ID == id);
            if (foundFood is null)
                return Task.CompletedTask;

            _petFoods.Remove(foundFood);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id) {
            DeleteLogic(id);
            return Task.CompletedTask;
        }        

        public Task<IEnumerable<PetFood>> GetAllAsync() {
            return Task.FromResult(_petFoods.AsEnumerable());
        }

        public PetFood? GetById(int id)
        {
            return _petFoods.SingleOrDefault(petFood => petFood.ID == id);
        }

        public Task<PetFood?> GetByIdAsync(int id) {
            return Task.FromResult(_petFoods.SingleOrDefault(petFood => petFood.ID == id));
        }

        public Task Update(int id, PetFood entity)
        {
            var foundFood = _petFoods.SingleOrDefault(pet => pet.ID == id);
            if (foundFood is null)
                return Task.CompletedTask;

            foundFood.AnimalType = entity.AnimalType;
            foundFood.Price = entity.Price;
            foundFood.Cost = entity.Cost;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(int id, PetFood entity) {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }

        private void DeleteLogic(int id)
        {
            var foundPetFood = _petFoods.SingleOrDefault(todo => todo.ID == id);
            if (foundPetFood is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found");

            _petFoods.Remove(foundPetFood);
        }

        private void UpdateLogic(int id, PetFood entity)
        {
            var foundPetFood= _petFoods.SingleOrDefault(todo => todo.ID == id);
            if (foundPetFood is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found");

            foundPetFood.AnimalType = entity.AnimalType;
            foundPetFood.Price = entity.Price;
            foundPetFood.Cost = entity.Cost;
        }

        private void AddLogic(PetFood entity)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            var lastId = _petFoods.OrderBy(petFood => petFood.ID).Last().ID;
            entity.ID = ++lastId;
            _petFoods.Add(entity);
        }
    }
}
