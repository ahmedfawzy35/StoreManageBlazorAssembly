using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class BrancheMoneySafe
    {
        public BrancheMoneySafe()
        {
            CashInFromBrancheMoneySaves = new HashSet<CashInFromBrancheMoneySafe>();
            CashOutToBrancheMoneySaves = new HashSet<CashOutToBrancheMoneySafe>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public double StartAccount { get; set; }
        public int BrancheId { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual ICollection<CashInFromBrancheMoneySafe> CashInFromBrancheMoneySaves { get; set; }
        public virtual ICollection<CashOutToBrancheMoneySafe> CashOutToBrancheMoneySaves { get; set; }
    }
}
