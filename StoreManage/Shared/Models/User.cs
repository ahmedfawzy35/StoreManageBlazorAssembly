using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class User
    {
        public User()
        {
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
            EmployeeIncreases = new HashSet<EmployeeIncrease>();
            EmployeeLesses = new HashSet<EmployeeLess>();
            EmployeePenalties = new HashSet<EmployeePenalty>();
            EmployeeRewards = new HashSet<EmployeeReward>();
            OrderDetailsEditHistories = new HashSet<OrderDetailsEditHistory>();
            OrderEditHistories = new HashSet<OrderEditHistory>();
            SellerAddingSettlements = new HashSet<SellerAddingSettlement>();
            SellerDiscountSettlements = new HashSet<SellerDiscountSettlement>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Enabled { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? IdUserDeleIt { get; set; }
        public bool? IsDeleted { get; set; }
        public int EditCount { get; set; }
        public bool? IsEdit { get; set; }

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
        public virtual ICollection<EmployeeIncrease> EmployeeIncreases { get; set; }
        public virtual ICollection<EmployeeLess> EmployeeLesses { get; set; }
        public virtual ICollection<EmployeePenalty> EmployeePenalties { get; set; }
        public virtual ICollection<EmployeeReward> EmployeeRewards { get; set; }
        public virtual ICollection<OrderDetailsEditHistory> OrderDetailsEditHistories { get; set; }
        public virtual ICollection<OrderEditHistory> OrderEditHistories { get; set; }
        public virtual ICollection<SellerAddingSettlement> SellerAddingSettlements { get; set; }
        public virtual ICollection<SellerDiscountSettlement> SellerDiscountSettlements { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
