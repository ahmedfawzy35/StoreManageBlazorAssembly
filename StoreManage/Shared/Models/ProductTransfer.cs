using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class ProductTransfer
    {
        public ProductTransfer()
        {
            ProductTransferDetails = new HashSet<ProductTransferDetail>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public int BrancheIdFrom { get; set; }
        public int BrancheIdTo { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<ProductTransferDetail> ProductTransferDetails { get; set; }
    }
}
