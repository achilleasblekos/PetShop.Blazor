using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repositories;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IEntityRepo<Customer> _customerRepo;
        //private readonly IEntityRepo<Commenter> _commenterRepo;

        public CustomerController(IEntityRepo<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
            
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerListViewModel>> Get()
        {
            var result = await _customerRepo.GetAllAsync();
            return result.Select(x => new CustomerListViewModel
            {
                ID =x.ID,
                Name= x.Name,
                Surname=x.Surname,
                Phone=x.Phone,
                TIN = x.TIN,
            });
        }

        [HttpGet("{id}")]
        public async Task<CustomerEditViewModel> Get(int id)
        {
            CustomerEditViewModel model = new();
            if (id != 0)
            {
                var existing = await _customerRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Name = existing.Name;
                model.Surname = existing.Surname;
                model.Phone = existing.Phone;
                model.TIN = existing.TIN;
                //model.Comments = existing.Comments.Select(comment => new CustomerEditCommentViewModel
                //{
                //    Id = comment.Id,
                //    Text = comment.Text,
                //    CommenterId = comment.CommenterId
                //}).ToList();
            }

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
            await _customerRepo.DeleteAsync(id);
        }



        [HttpPost]
        public async Task Post(CustomerEditViewModel customer)
        {
            var newCustomer = new Customer()
            {
                ID=customer.ID,
                Surname = customer.Surname,
                Name = customer.Name,
                Phone = customer.Phone,
                TIN= customer.TIN,
                
            };
            //foreach (var comment in customer.Comments)
            //{
            //    newCustomer.Comments.Add(new CustomerComment(comment.Text)
            //    {
            //        CommenterId = comment.CommenterId
            //    });
            //}
            await _customerRepo.AddAsync(newCustomer);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CustomerEditViewModel customer)
        {
            var itemToUpdate = await _customerRepo.GetByIdAsync(customer.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.Name = customer.Name;
            itemToUpdate.Surname = customer.Surname;
            itemToUpdate.Phone = customer.Phone;
            itemToUpdate.TIN = customer.TIN;
            
            //itemToUpdate.Comments = customer.Comments.Select(comment => new CustomerComment(comment.Text)
            //{
            //    Id = comment.Id,
            //    CommenterId = comment.CommenterId
            //}).ToList();

            await _customerRepo.UpdateAsync(customer.ID, itemToUpdate);

            return Ok();
        }
    }
}

