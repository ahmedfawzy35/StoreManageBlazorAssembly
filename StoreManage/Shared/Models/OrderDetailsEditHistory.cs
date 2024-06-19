using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class OrderDetailsEditHistory
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string? ModelType { get; set; }
        public int OrderPurchaseId { get; set; }
        public int OldQte { get; set; }
        public double OldPrice { get; set; }
        public double OldDiscount { get; set; }
        public int OldProductId { get; set; }
        public int BrancheId { get; set; }
        public int EditUserId { get; set; }
        public DateTime DateEdit { get; set; }
        public int NewQte { get; set; }
        public double NewPrice { get; set; }
        public double NewDiscount { get; set; }
        public int NewProductId { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual User EditUser { get; set; } = null!;
    }
}
