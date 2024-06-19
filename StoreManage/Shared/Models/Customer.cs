using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CashInFromCustomers = new HashSet<CashInFromCustomer>();
            CustomerAddingSettlements = new HashSet<CustomerAddingSettlement>();
            CustomerDiscountSettlements = new HashSet<CustomerDiscountSettlement>();
            CustomerPhones = new HashSet<CustomerPhone>();
            OrderBacks = new HashSet<OrderBack>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adress { get; set; }
        public double StartAccount { get; set; }
        public int BrancheId { get; set; }
        public int CustomertypeId { get; set; }
        public bool? StopDealing { get; set; }

     
        public virtual Branche? Branche { get; set; } = null!;
        public virtual CustomerType? Customertype { get; set; } = null!;
        public virtual ICollection<CashInFromCustomer> CashInFromCustomers { get; set; }
        public virtual ICollection<CustomerAddingSettlement> CustomerAddingSettlements { get; set; }
        public virtual ICollection<CustomerDiscountSettlement> CustomerDiscountSettlements { get; set; }
        public virtual ICollection<CustomerPhone> CustomerPhones { get; set; }
        public virtual ICollection<OrderBack> OrderBacks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual double CustomerAccount => StartAccount
                                              + (Orders == null ? 0 : Orders.Where(c => c.IsDeleted == false).Sum(x => x.RemainingAmount))
                                              - (OrderBacks == null ? 0 : OrderBacks.Where(c => c.IsDeleted == false).Sum(x => x.RemainingAmount))
                                              - (CashInFromCustomers == null ? 0 : CashInFromCustomers.Where(c => c.IsDeleted == false).Sum(x => x.Value))
                                              + (CustomerAddingSettlements == null ? 0 : CustomerAddingSettlements.Sum(x => x.Value))
                                              - (CustomerDiscountSettlements == null ? 0 : CustomerDiscountSettlements.Sum(x => x.Value));

    }
}
