using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_DataAccess
{
    public partial class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public byte Level { get; set; }
        public int Order { get; set; }
        public bool Deleted { get; set; }
    }
}
