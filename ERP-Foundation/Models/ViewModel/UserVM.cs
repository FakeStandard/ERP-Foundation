using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Models.ViewModel
{
    public class UserVM
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Department { get; set; }
        public string Unit { get; set; }
        public string Position { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTel { get; set; }
        public string Status { get; set; }
    }
}
