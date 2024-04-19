using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class CustomerPhone
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Phone { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
