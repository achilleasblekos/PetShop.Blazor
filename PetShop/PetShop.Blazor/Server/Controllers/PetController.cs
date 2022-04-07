using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repositories;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IEntityRepo<Pet> _petRepo;

        public PetController(IEntityRepo<Pet> petRepo)
        {
            _petRepo = petRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<PetListViewModel>> Get()
        {
            var result = await _petRepo.GetAllAsync();
            return result.Select(x => new PetListViewModel
            {
                ID = x.ID,
                Breed = x.Breed,
                AnimalType = x.AnimalType,
                PetStatus=x.PetStatus,
                Cost=x.Cost,
                Price= x.Price,
            });
        }
        [HttpGet("{id}")]
        public async Task<PetEditViewModel> Get(int id)
        {
            PetEditViewModel model = new();
            if (id != 0)
            {
                var existing = await _petRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Breed = existing.Breed;
                model.AnimalType = existing.AnimalType;
                model.PetStatus = existing.PetStatus;
                model.Price = existing.Price;
                model.Cost = existing.Cost;
                
            }
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _petRepo.DeleteAsync(id);
        }

        [HttpPost]
        public async Task Post(PetListViewModel pet)
        {
            var newPet = new Pet()
            {
                ID = pet.ID,
                Breed = pet.Breed,
                AnimalType = pet.AnimalType,
                PetStatus = pet.PetStatus,
                Price = pet.Price,
                Cost = pet.Cost
            };
            await _petRepo.AddAsync(newPet);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PetListViewModel pet)
        {
            var itemToUpdate = await _petRepo.GetByIdAsync(pet.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.PetStatus = pet.PetStatus;
            itemToUpdate.Cost = pet.Cost;
            itemToUpdate.Price = pet.Price;
            itemToUpdate.ID = pet.ID;
            itemToUpdate.Breed = pet.Breed;
            itemToUpdate.AnimalType = pet.AnimalType;
            await _petRepo.UpdateAsync(pet.ID, itemToUpdate);

            return Ok();
        }
    }
}
