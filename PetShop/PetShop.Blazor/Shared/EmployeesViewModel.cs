using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Model;

namespace PetShop.Blazor.Shared {
    public class EmployeesViewModel {

        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(99, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long.")]
        public string Name { get; set; }

        [Required]
        [StringLength(99, MinimumLength = 2, ErrorMessage = "Must be at least 2 characters long.")]
        public string Surname { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }

        [Required]
        [Range(0.00, 10000.00, ErrorMessage = "Must be between 0.0 and 10000.00")] // int?
        public decimal SallaryPerMonth { get; set; }
    }
}
