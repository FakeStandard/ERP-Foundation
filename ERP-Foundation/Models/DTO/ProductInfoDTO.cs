using ERP_Foundation.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Models.DTO
{
    public class ProductInfoDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public List<ProductVM> Data { get; set; }
    }
}
