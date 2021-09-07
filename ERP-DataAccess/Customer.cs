using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_DataAccess
{
    public partial class Customer
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
        public DateTime CreateTime { get; set; }
        public int AddedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Modifier { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int? DeletedBy { get; set; }
    }
}
