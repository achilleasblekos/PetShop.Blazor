using PetShop.EF.Repositories;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.MockRepositories
{
    public class MockPetRepo : IEntityRepo<Pet>
    {
        private List<Pet> _pets = new List<Pet>
        {
            new Pet(){ID = 1, Breed="German Shepherd",AnimalType=AnimalType.Mammal,PetStatus=PetStatus.OK,Price=300,Cost=100},
            new Pet(){ID = 2, Breed="Canery",AnimalType=AnimalType.Bird,PetStatus=PetStatus.Recovering,Price=50,Cost=10},
            //------------------------------------------------------------------------------TO BUY PET IS OK :P----------------------
            new Pet(){ID = 3, Breed="Royal Cobra",AnimalType=AnimalType.Reptile,PetStatus=PetStatus.Unhealthy,Price=3000,Cost=500},
            //-----------------------------------------------------REALLY UNHEALTHY FORGETS OFTEN BUT EXPENSIVE (MOVIE STAR)------
            new Pet(){ID = 4, Breed="Dory",AnimalType=AnimalType.Fish,PetStatus=PetStatus.Unhealthy,Price=5000,Cost=500},
        };

        public Task AddAsync(Pet entity) {
            AddLogic(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id) {
            DeleteLogic(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Pet>> GetAllAsync() {
            return Task.FromResult(_pets.AsEnumerable());
        }

        public Task<Pet?> GetByIdAsync(int id) {
            return Task.FromResult(_pets.SingleOrDefault(pet => pet.ID == id));
        }

        public Task UpdateAsync(int id, Pet entity) {
            UpdateLogic(id, entity);
            return Task.CompletedTask;
        }

        private void AddLogic(Pet entity)
        {
            if (entity.ID != 0)
                throw new ArgumentException("Given entity should not have ID set", nameof(entity));

            var lastId = _pets.OrderBy(pet => pet.ID).Last().ID;
            entity.ID = ++lastId;
            _pets.Add(entity);
        }

        private void DeleteLogic(int id)
        {
            var foundPet = _pets.SingleOrDefault(pet => pet.ID == id);
            if (foundPet is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _pets.Remove(foundPet);
        }

        private void UpdateLogic(int id, Pet entity)
        {
            var foundPet = _pets.SingleOrDefault(pet => pet.ID == id);
            if (foundPet is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            foundPet.Breed = entity.Breed;
            foundPet.AnimalType = entity.AnimalType;
            foundPet.PetStatus = entity.PetStatus;
            foundPet.Price = entity.Price;
            foundPet.Cost = entity.Cost;
        }
    }
}
