using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Seller
    {
        public Seller()
        {
            CashOutToSellers = new HashSet<CashOutToSeller>();
            PurchaseBacks = new HashSet<PurchaseBack>();
            Purchases = new HashSet<Purchase>();
            SellerAddingSettlements = new HashSet<SellerAddingSettlement>();
            SellerDiscountSettlements = new HashSet<SellerDiscountSettlement>();
            SellerPhones = new HashSet<SellerPhone>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adress { get; set; }
        public double StartAccount { get; set; }
        public int BrancheId { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual ICollection<CashOutToSeller> CashOutToSellers { get; set; }
        public virtual ICollection<PurchaseBack> PurchaseBacks { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<SellerAddingSettlement> SellerAddingSettlements { get; set; }
        public virtual ICollection<SellerDiscountSettlement> SellerDiscountSettlements { get; set; }
        public virtual ICollection<SellerPhone> SellerPhones { get; set; }
    }
}
