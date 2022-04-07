using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repositories;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    { 
            private readonly IEntityRepo<Transaction> _transactionRepo;
            //private readonly IEntityRepo<Commenter> _commenterRepo;

            public TransactionController(IEntityRepo<Transaction> transactionRepo)
            {
                _transactionRepo = transactionRepo;

            }

            [HttpGet]
            public async Task<IEnumerable<TransactionListViewModel>> Get()
            {
                var result = await _transactionRepo.GetAllAsync();
                return result.Select(x => new TransactionListViewModel
                {
                    Date = x.Date,
                    CustomerID = x.CustomerID,
                    EmployeeID = x.EmployeeID,
                    PetFoodID = x.PetFoodID,
                    PetFoodPrice = x.PetFoodPrice,
                    PetFoodQty = x.PetFoodQty,
                    PetID = x.PetID,
                    PetPrice = x.PetPrice,
                    TotalPrice = x.TotalPrice,
                });
            }

        [HttpGet("{id}")]
            public async Task<TransactionEditViewModel> Get(int id)
            {
            TransactionEditViewModel model = new();
            if (id != 0)
            {
                var existing = await _transactionRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Customer = existing.Customer;
                model.Employee = existing.Employee;
                model.Pet = existing.Pet;
                //model.PetFoods=....
            }

        
                    //model.Comments = existing.Comments.Select(comment => new CustomerEditCommentViewModel
                    //{
                    //    Id = comment.Id,
                    //    Text = comment.Text,
                    //    CommenterId = comment.CommenterId
                    //}).ToList();
                

                //var commenters = await _commenterRepo.GetAllAsync();
                //model.Commenters = commenters.Select(x => new CustomerEditCommenterViewModel
                //{
                //    Id = x.Id,
                //    Name = x.Name
                //}).ToList();

                return model;
            }

            [HttpDelete("{id}")]
            public async Task Delete(int id)
            {
                await _transactionRepo.DeleteAsync(id);
            }



            [HttpPost]
            public async Task Post(TransactionEditViewModel transaction)
            {
                var newTransaction = new Transaction()
                {
                    //ID = transaction.ID,
                    //Date = transaction.
                    //Surname = transaction.Surname,
                    //Name = transaction.Name,
                    //Phone = transaction.Phone,
                    //TIN = transaction.TIN,

                };
                //foreach (var comment in transaction.Comments)
                //{
                //    newTransaction.Comments.Add(new CustomerComment(comment.Text)
                //    {
                //        CommenterId = comment.CommenterId
                //    });
                //}
                await _transactionRepo.AddAsync(newTransaction);
            }

            [HttpPut]
            public async Task<ActionResult> Put(TransactionEditViewModel transaction)
            {
                var itemToUpdate = await _transactionRepo.GetByIdAsync(transaction.ID);
                if (itemToUpdate == null) return NotFound();

            itemToUpdate.Customer = transaction.Customer;
            itemToUpdate.Employee = transaction.Employee;
            itemToUpdate.Pet = transaction.Pet;
            //itemToUpdate.PetFoods =transaction.PetFoods.Select(petFood => new TransactionEditPetFoodViewModel()
            //{
            //    AnimalType= petFood.AnimalType,
            //    Cost=petFood.Cost,
            //    Price=petFood.Price,
            //    ID= petFood.ID,

            //}).ToList();

            //itemToUpdate.Comments = transaction.Comments.Select(comment => new CustomerComment(comment.Text)
            //{
            //    Id = comment.Id,
            //    CommenterId = comment.CommenterId
            //}).ToList();

            await _transactionRepo.UpdateAsync(transaction.ID, itemToUpdate);

                return Ok();
            }
    }
}

