using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Branche
    {
        public Branche()
        {
            BrancheMoneySaves = new HashSet<BrancheMoneySafe>();
            CashDayCloses = new HashSet<CashDayClose>();
            CashEditHistories = new HashSet<CashEditHistory>();
            CashInFromBankAccounts = new HashSet<CashInFromBankAccount>();
            CashInFromBrancheMoneySaves = new HashSet<CashInFromBrancheMoneySafe>();
            CashInFromCustomers = new HashSet<CashInFromCustomer>();
            CashInFromIncomes = new HashSet<CashInFromIncome>();
            CashInFromMasterMoneySaves = new HashSet<CashInFromMasterMoneySafe>();
            CashOutToAdvancepaymentOfSalaries = new HashSet<CashOutToAdvancepaymentOfSalary>();
            CashOutToBankAccounts = new HashSet<CashOutToBankAccount>();
            CashOutToBrancheMoneySaves = new HashSet<CashOutToBrancheMoneySafe>();
            CashOutToMasterMoneySaves = new HashSet<CashOutToMasterMoneySafe>();
            CashOutToOutGoings = new HashSet<CashOutToOutGoing>();
            CashOutToSalaries = new HashSet<CashOutToSalary>();
            CashOutToSellers = new HashSet<CashOutToSeller>();
            CustomerAddingSettlements = new HashSet<CustomerAddingSettlement>();
            CustomerDiscountSettlements = new HashSet<CustomerDiscountSettlement>();
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            InComes = new HashSet<InCome>();
            OrderBacks = new HashSet<OrderBack>();
            OrderDetailsEditHistories = new HashSet<OrderDetailsEditHistory>();
            OrderEditHistories = new HashSet<OrderEditHistory>();
            Orders = new HashSet<Order>();
            OutGoings = new HashSet<OutGoing>();
            Products = new HashSet<Product>();
            PurchaseBacks = new HashSet<PurchaseBack>();
            Purchases = new HashSet<Purchase>();
            SellerAddingSettlements = new HashSet<SellerAddingSettlement>();
            SellerDiscountSettlements = new HashSet<SellerDiscountSettlement>();
            Sellers = new HashSet<Seller>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public string? Phone { get; set; }
        public string? Adress { get; set; }
        public double StartAccount { get; set; }

        public virtual ICollection<BrancheMoneySafe> BrancheMoneySaves { get; set; }
        public virtual ICollection<CashDayClose> CashDayCloses { get; set; }
        public virtual ICollection<CashEditHistory> CashEditHistories { get; set; }
        public virtual ICollection<CashInFromBankAccount> CashInFromBankAccounts { get; set; }
        public virtual ICollection<CashInFromBrancheMoneySafe> CashInFromBrancheMoneySaves { get; set; }
        public virtual ICollection<CashInFromCustomer> CashInFromCustomers { get; set; }
        public virtual ICollection<CashInFromIncome> CashInFromIncomes { get; set; }
        public virtual ICollection<CashInFromMasterMoneySafe> CashInFromMasterMoneySaves { get; set; }
        public virtual ICollection<CashOutToAdvancepaymentOfSalary> CashOutToAdvancepaymentOfSalaries { get; set; }
        public virtual ICollection<CashOutToBankAccount> CashOutToBankAccounts { get; set; }
        public virtual ICollection<CashOutToBrancheMoneySafe> CashOutToBrancheMoneySaves { get; set; }
        public virtual ICollection<CashOutToMasterMoneySafe> CashOutToMasterMoneySaves { get; set; }
        public virtual ICollection<CashOutToOutGoing> CashOutToOutGoings { get; set; }
        public virtual ICollection<CashOutToSalary> CashOutToSalaries { get; set; }
        public virtual ICollection<CashOutToSeller> CashOutToSellers { get; set; }
        public virtual ICollection<CustomerAddingSettlement> CustomerAddingSettlements { get; set; }
        public virtual ICollection<CustomerDiscountSettlement> CustomerDiscountSettlements { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<InCome> InComes { get; set; }
        public virtual ICollection<OrderBack> OrderBacks { get; set; }
        public virtual ICollection<OrderDetailsEditHistory> OrderDetailsEditHistories { get; set; }
        public virtual ICollection<OrderEditHistory> OrderEditHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OutGoing> OutGoings { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseBack> PurchaseBacks { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<SellerAddingSettlement> SellerAddingSettlements { get; set; }
        public virtual ICollection<SellerDiscountSettlement> SellerDiscountSettlements { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
    }
}
