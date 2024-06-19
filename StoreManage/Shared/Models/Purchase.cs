using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SellerId { get; set; }
        public double Total { get; set; }
        public double Paid { get; set; }
        public double Discount { get; set; }
        public double RemainingAmount { get; set; }
        public int BrancheId { get; set; }
        public string? Notes { get; set; }
        public int OrderNumber { get; set; }
        public double OrderProfit { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int EditCount { get; set; }
        public int? IdUserDeleIt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEdit { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
