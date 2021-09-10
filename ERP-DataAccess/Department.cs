using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_DataAccess
{
    public partial class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public DateTime CreateTime { get; set; }
        public int AddedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Modifier { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int? DeletedBy { get; set; }
    }
}
