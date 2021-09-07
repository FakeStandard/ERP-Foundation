using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Models.ViewModel
{
    public class CustomerVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTel { get; set; }
        public int PaymentRule { get; set; }
    }
}
