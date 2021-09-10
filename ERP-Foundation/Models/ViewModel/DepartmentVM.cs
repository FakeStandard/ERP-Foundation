using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Models.ViewModel
{
    public class DepartmentVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
    }
}
