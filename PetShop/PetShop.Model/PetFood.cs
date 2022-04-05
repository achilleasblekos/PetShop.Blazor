using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Model
{
    public class PetFood: Product
    {
        //Relations
        public List<Transaction> Transactions { get; set; }
        public PetFood()
        {

        }
    }
}
