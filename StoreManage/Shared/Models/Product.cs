using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderBackDetails = new HashSet<OrderBackDetail>();
            OrderDetails = new HashSet<OrderDetail>();
            PurchaseBackDetails = new HashSet<PurchaseBackDetail>();
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public int Id { get; set; }
        public string? Barcode { get; set; } = new Guid().ToString();

        public string? Name { get; set; } = null!;
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
        public double Stock { get; set; }
        public DateTime? LastUpdate { get; set; } = DateTime.Now;
        public virtual Branche Branche { get; set; } = null!;
        public virtual Catogry catogry { get; set; } = null!;
        public virtual ICollection<OrderBackDetail>? OrderBackDetails { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<PurchaseBackDetail>? PurchaseBackDetails { get; set; }
        public virtual ICollection<PurchaseDetail>? PurchaseDetails { get; set; }
        public virtual IEnumerable<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();

        public virtual int OrderQte => OrderDetails == null ? 0 : OrderDetails.Sum(x => x.Qte);
        public virtual int OrdersBackQte => OrderBackDetails == null ? 0 : OrderBackDetails.Sum(x => x.Qte);
        public virtual int PurchaseQte => PurchaseDetails == null ? 0 : PurchaseDetails.Sum(x => x.Qte);
        public virtual int PurchaseBackQte => PurchaseBackDetails == null ? 0 : PurchaseBackDetails.Sum(x => x.Qte);

    }
}
