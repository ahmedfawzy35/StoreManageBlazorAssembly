using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class SellerPhone
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string? Phone { get; set; }

        public virtual Seller Seller { get; set; } = null!;
    }
}
