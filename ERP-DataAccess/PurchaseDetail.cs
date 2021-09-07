using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_DataAccess
{
    public partial class PurchaseDetail
    {
        public int ID { get; set; }
        public int PurchaseID { get; set; }
        public string ItemNo { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int AddedBy { get; set; }
        public int? Modifier { get; set; }
        public int? DeletedBy { get; set; }
    }
}
