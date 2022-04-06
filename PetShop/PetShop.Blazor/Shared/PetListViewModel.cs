using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PetShop.Blazor.Shared
{
    public class PetListViewModel
    {
        public int ID { get; set; }
        public string? Breed { get; set; }
        public PetStatus PetStatus { get; set; }
        public AnimalType AnimalType { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }


    }
}
