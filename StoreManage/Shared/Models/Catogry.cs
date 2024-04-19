using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Catogry
    {
        public Catogry()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Details { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
