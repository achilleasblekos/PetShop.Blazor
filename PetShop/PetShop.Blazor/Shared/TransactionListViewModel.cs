using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class TransactionListViewModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int? PetID { get; set; }
        public decimal? PetPrice { get; set; }
        public int PetFoodID { get; set; }
        public int PetFoodQty { get; set; }
        public decimal PetFoodPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class TransactionEditViewModel
    { 
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Pet Pet { get; set; }
        public List<TransactionEditPetFoodViewModel> PetFoods { get; set; } = new();
    }

    public class TransactionEditPetFoodViewModel
    {
        public int ID { get; set; }
        public AnimalType AnimalType { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }

    public class TransactionEditCustomerViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TIN { get; set; }
    }
}
         
    

