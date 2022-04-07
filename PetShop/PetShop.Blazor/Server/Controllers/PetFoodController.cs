using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repositories;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetFoodController : ControllerBase
    {
        private readonly IEntityRepo<PetFood> _petfoodRepo;
        //private readonly IEntityRepo<Commenter> _commenterRepo;

        public PetFoodController(IEntityRepo<PetFood> petfoodRepo)
        {
            _petfoodRepo = petfoodRepo;

        }

        [HttpGet]
        public async Task<IEnumerable<PetFoodListViewModel>> Get()
        {
            var result = await _petfoodRepo.GetAllAsync();
            return result.Select(x => new PetFoodListViewModel
            {
                ID = x.ID,
                AnimalType = x.AnimalType,                
                Price = x.Price,
                Cost = x.Cost,
            });
        }

        [HttpGet("{id}")]
        public async Task<PetFoodEditViewModel> Get(int id)
        {
            PetFoodEditViewModel model = new();
            if (id != 0)
            {
                var existing = await _petfoodRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.AnimalType = existing.AnimalType;                
                model.Price = existing.Price;
                model.Cost = existing.Cost;
                //model.Comments = existing.Comments.Select(comment => new PetFoodEditCommentViewModel
                //{
                //    Id = comment.Id,
                //    Text = comment.Text,
                //    CommenterId = comment.CommenterId
                //}).ToList();
            }

            //var commenters = await _commenterRepo.GetAllAsync();
            //model.Commenters = commenters.Select(x => new PetFoodEditCommenterViewModel
            //{
            //    Id = x.Id,
            //    AnimalType = x.AnimalType
            //}).ToList();

            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _petfoodRepo.DeleteAsync(id);
        }



        [HttpPost]
        public async Task Post(PetFoodEditViewModel petfood)
        {
            var newPetFood = new PetFood()
            {
                ID = petfood.ID,                
                AnimalType = petfood.AnimalType,
                Price = petfood.Price,
                Cost = petfood.Cost,

            };
            //foreach (var comment in petfood.Comments)
            //{
            //    newPetFood.Comments.Add(new PetFoodComment(comment.Text)
            //    {
            //        CommenterId = comment.CommenterId
            //    });
            //}
            await _petfoodRepo.AddAsync(newPetFood);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PetFoodEditViewModel petfood)
        {
            var itemToUpdate = await _petfoodRepo.GetByIdAsync(petfood.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.AnimalType = petfood.AnimalType;            
            itemToUpdate.Price = petfood.Price;
            itemToUpdate.Cost = petfood.Cost;

            //itemToUpdate.Comments = petfood.Comments.Select(comment => new PetFoodComment(comment.Text)
            //{
            //    Id = comment.Id,
            //    CommenterId = comment.CommenterId
            //}).ToList();

            await _petfoodRepo.UpdateAsync(petfood.ID, itemToUpdate);

            return Ok();
        }
    }
}
