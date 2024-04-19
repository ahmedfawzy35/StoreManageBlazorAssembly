using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class OrderEditHistory
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string? ModelType { get; set; }
        public DateTime OldDate { get; set; }
        public double OldTotal { get; set; }
        public double OldPaid { get; set; }
        public double OldDiscount { get; set; }
        public double OldRemainingAmount { get; set; }
        public string? OldNotes { get; set; }
        public int BrancheId { get; set; }
        public int EditUserId { get; set; }
        public DateTime DateEdit { get; set; }
        public DateTime NewDate { get; set; }
        public double NewTotal { get; set; }
        public double NewPaid { get; set; }
        public double NewDiscount { get; set; }
        public double NewRemainingAmount { get; set; }
        public string? NewNotes { get; set; }
        public int NewForeignKey { get; set; }
        public int OldForeignKey { get; set; }

        public virtual Branch Branche { get; set; } = null!;
        public virtual User EditUser { get; set; } = null!;
    }
}
