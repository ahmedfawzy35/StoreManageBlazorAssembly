using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class CashInFromIncome
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string? Notes { get; set; }
        public int InComeId { get; set; }
        public int UserId { get; set; }
        public int BrancheId { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int EditCount { get; set; }
        public int? IdUserDeleIt { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsEdit { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual InCome InCome { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
