using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Model
{
    public class Pet: Product
    {
        public string Breed { get; set; }     

        public PetStatus PetStatus { get; set; }

        //Relations
        public Transaction Transaction { get; set; }
        public Pet()
        {

        }
    }
}
