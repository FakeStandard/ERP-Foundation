using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_DataAccess
{
    public partial class User
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public int Department { get; set; }
        public int Unit { get; set; }
        public int Position { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactTel { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public int AddedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Modifier { get; set; }
    }
}
