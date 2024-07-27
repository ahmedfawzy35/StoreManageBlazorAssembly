using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Barcode { get; set; } = new Guid().ToString();

        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public int StartStock { get; set; } = 0;
        public double LastPurchasePrice { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public int LimitStock { get; set; }
        public int CatogryId { get; set; }
        public bool ShowInBill { get; set; }=true;
        public int BrancheId { get; set; }
        public string? CustomId { get; set; }
        public double Stock { get; set; }

        public  string? BrancheName { get; set; } = null!;
        public  string? CatogryName { get; set; } = null!;
       
    }
}
