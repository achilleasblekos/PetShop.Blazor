﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class CustomerListViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string  Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    }

    public class CustomerEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    }
}

