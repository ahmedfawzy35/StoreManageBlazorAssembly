using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string? Barcode { get; set; }
        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public int StartStock { get; set; }
        public double LastPurchasePrice { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public int LimitStock { get; set; }
        public int CatogryId { get; set; }
        public bool ShowInBill { get; set; }
        public int BrancheId { get; set; }
        public string? CustomId { get; set; }
        public DateTime? LastUpdate { get; set; }
    }

}
