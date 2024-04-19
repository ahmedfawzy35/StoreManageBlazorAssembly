using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class OutGoing
    {
        public OutGoing()
        {
            CashOutToOutGoings = new HashSet<CashOutToOutGoing>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public int BrancheId { get; set; }

        public virtual Branch Branche { get; set; } = null!;
        public virtual ICollection<CashOutToOutGoing> CashOutToOutGoings { get; set; }
    }
}
