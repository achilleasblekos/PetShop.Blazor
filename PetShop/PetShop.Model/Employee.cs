using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Model
{
    public class Employee: BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public decimal SallaryPerMonth { get; set; }
        //public decimal SallaryPerMonth { get; set; }

        //Relations
        public List<Transaction> Transactions { get; set; }

        public Employee()
        {

        }
    }
}
