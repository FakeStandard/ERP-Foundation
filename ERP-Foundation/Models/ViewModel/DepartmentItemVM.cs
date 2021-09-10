using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Models.ViewModel
{
    public class DepartmentItemVM
    {
        public int key { get; set; }
        public string value { get; set; }

        public List<DepartmentItemVM> units { get; set; }
    }
}
