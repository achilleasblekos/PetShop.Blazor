using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repositories;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase {

        private readonly IEntityRepo<Employee> _employeeRepo;
        private readonly IEntityRepo<Transaction> _transactionRepo;

        public EmployeeController(IEntityRepo<Employee> employeeRepo, IEntityRepo<Transaction> transactionRepo) {
            _employeeRepo = employeeRepo;
            _transactionRepo = transactionRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeListViewModel>> Get() { //GetAll
            var result = await _employeeRepo.GetAllAsync();
            return result.Select(x => new EmployeeListViewModel { // use Select to convert them to ViewModel
                ID = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                EmployeeType = x.EmployeeType,
                SallaryPerMonth = x.SallaryPerMonth
            });
        }

        [HttpGet("{id}")]
        public async Task<EmployeeEditViewModel> Get(int id) {
            EmployeeEditViewModel model = new();
            if (id != 0) {
                var existing = await _employeeRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Name = existing.Name;
                model.Surname = existing.Surname;
                model.EmployeeType = existing.EmployeeType;
                model.SallaryPerMonth = existing.SallaryPerMonth;
                //model.Transactions = existing.Transactions.Select(transaction => new TransactionEditViewModel
                //{
                //    Id = transaction.Id,
                //    Text = comment.Text,
                //    CommenterId = comment.CommenterId
                //}).ToList();

                //var transactions = await _transactionRepo.GetAllAsync();
                //model.Transactions = transactions.Select(x => new TodoEditCommenterViewModel {
                //    ID = x.Id,
                //    
                //}).ToList();
            }
            return model;
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(int id) {
            await _employeeRepo.DeleteAsync(id);
        }

        [HttpPost] //[HttpPost("newRoute")] custom route if I want
        public async Task Post(EmployeeEditViewModel employee) { //create // 404
            var newEmployee = new Employee() {
                ID = employee.ID,
                Surname = employee.Surname,
                Name = employee.Name,
                EmployeeType = employee.EmployeeType,
                SallaryPerMonth = employee.SallaryPerMonth
            };
            // 1 new transaction
            // 1 employee,
            // 1 customer
            // possible multiple petfoods,
            // possibly 1 pet
            //    newEmployee.Transactions.Add(new Transaction()
            //    {
            //        //transaction stuff
            //    });
            //  NOT HERE
            await _employeeRepo.AddAsync(newEmployee);
        }


        [HttpPut]
        public async Task<ActionResult> Put(EmployeeListViewModel employee) {
            var itemToUpdate = await _employeeRepo.GetByIdAsync(employee.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.Name = employee.Name;
            itemToUpdate.Surname = employee.Surname;
            itemToUpdate.EmployeeType = employee.EmployeeType;
            itemToUpdate.SallaryPerMonth = employee.SallaryPerMonth;
            //itemToUpdate.Transactions = employee.Transactions.Select(transaction => new Transaction() { // those transactions would come from 
            //    ID = transaction.ID,                                                                    //  the Client, so probably not this way
            //    CommenterId = comment.CommenterId 
            //}).ToList();

            await _employeeRepo.UpdateAsync(employee.ID, itemToUpdate);
            return Ok();
        }
    }
}
