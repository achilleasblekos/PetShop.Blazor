using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Model
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        //TODO: PhoneAttribute
        public string Phone { get; set; }
        public string TIN { get; set; }

        //Relations
        public List<Transaction> Transactions { get; set; }



        public Customer()
        {

        }
    }
}
