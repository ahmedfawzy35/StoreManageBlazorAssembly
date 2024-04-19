using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class MasterMoneySafe
    {
        public MasterMoneySafe()
        {
            CashInFromMasterMoneySaves = new HashSet<CashInFromMasterMoneySafe>();
            CashOutToMasterMoneySaves = new HashSet<CashOutToMasterMoneySafe>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public double StartAccount { get; set; }

        public virtual ICollection<CashInFromMasterMoneySafe> CashInFromMasterMoneySaves { get; set; }
        public virtual ICollection<CashOutToMasterMoneySafe> CashOutToMasterMoneySaves { get; set; }
    }
}
