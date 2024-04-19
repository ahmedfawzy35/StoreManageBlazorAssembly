using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class BankAccount
    {
        public BankAccount()
        {
            CashInFromBankAccounts = new HashSet<CashInFromBankAccount>();
            CashOutToBankAccounts = new HashSet<CashOutToBankAccount>();
        }

        public int Id { get; set; }
        public string? BankName { get; set; }
        public string? BankBrancheName { get; set; }
        public string? BankAccountNumber { get; set; }
        public double StartAccount { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<CashInFromBankAccount> CashInFromBankAccounts { get; set; }
        public virtual ICollection<CashOutToBankAccount> CashOutToBankAccounts { get; set; }
    }
}
