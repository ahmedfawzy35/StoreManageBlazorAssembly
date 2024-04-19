using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class InCome
    {
        public InCome()
        {
            CashInFromIncomes = new HashSet<CashInFromIncome>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public int BrancheId { get; set; }

        public virtual Branch Branche { get; set; } = null!;
        public virtual ICollection<CashInFromIncome> CashInFromIncomes { get; set; }
    }
}
