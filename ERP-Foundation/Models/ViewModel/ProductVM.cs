using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Models.ViewModel
{
    public class ProductVM
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }
        public int Price { get; set; }

        public string Path { get; set; }
    }
}
