using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared {
    public class EmployeeListViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public decimal SallaryPerMonth { get; set; }
        //public List<TransactionListViewModel> Transactions { get; set; } = new();

    }

    public class EmployeeEditViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public decimal SallaryPerMonth { get; set; }
        //public List<TransactionListViewModel> Transactions { get; set; } = new();

    }
}
