using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_DataAccess
{
    public partial class Purchase
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public int Factory { get; set; }
        public int Payment { get; set; }
        public bool PaymenetStatus { get; set; }
        public int Total { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int Purchaser { get; set; }
        public int? Modifier { get; set; }
        public int? DeletedBy { get; set; }
    }
}
