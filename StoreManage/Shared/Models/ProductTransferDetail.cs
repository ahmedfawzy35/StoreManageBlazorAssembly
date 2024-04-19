using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class ProductTransferDetail
    {
        public int Id { get; set; }
        public int ProductTransferId { get; set; }
        public int ProductFromId { get; set; }
        public int ProductToId { get; set; }
        public int Qte { get; set; }
        public double Price { get; set; }

        public virtual ProductTransfer ProductTransfer { get; set; } = null!;
    }
}
