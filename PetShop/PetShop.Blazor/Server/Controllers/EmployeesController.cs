﻿using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repositories;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers {

    [ApiController]
    [Route("Controller")]
    public class EmployeesController : ControllerBase {

        private readonly IEntityRepo<Employee> _employeeRepo;
        public EmployeesController(IEntityRepo<Employee> employeeRepo) {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeesViewModel>> GetAll() { //GetAll
            var result = await _employeeRepo.GetAllAsync();
            return result.Select(x => new EmployeesViewModel { // use Select to convert them to ViewModel
                ID = x.ID,
                Name = x.Name,
                //Surname = x.Surname,
                //EmployeeType = x.EmployeeType,
                //SallaryPerMonth = x.SallaryPerMonth
            });
        }

        [HttpPost] //[HttpPost("newRoute")] custom route if I want
        public async Task Post(EmployeesViewModel employee) { //create // 404
            var newEmployee = new Employee();
            await _employeeRepo.AddAsync(newEmployee);  
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id) { 
            await _employeeRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(EmployeesViewModel employee) {
            var itemToUpdate = await _employeeRepo.GetByIdAsync(employee.ID);
            if (itemToUpdate == null) return NotFound();

            itemToUpdate.Name = employee.Name; 
            itemToUpdate.Surname = employee.Surname;
            itemToUpdate.EmployeeType = employee.EmployeeType;
            itemToUpdate.SallaryPerMonth = employee.SallaryPerMonth;

            await _employeeRepo.UpdateAsync(employee.ID, itemToUpdate);
            return Ok();
        }
    }
}
