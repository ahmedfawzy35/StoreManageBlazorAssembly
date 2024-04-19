using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class OrderBackDetail
    {
        public int Id { get; set; }
        public int OrderBackId { get; set; }
        public int ProductId { get; set; }
        public int Qte { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int EditCount { get; set; }
        public int? IdUserDeleIt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEdit { get; set; }

        public virtual OrderBack OrderBack { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
