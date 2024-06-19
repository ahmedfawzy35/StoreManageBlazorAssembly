using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class CashEditHistory
    {
        public int Id { get; set; }
        public string? ModelType { get; set; }
        public int ItemId { get; set; }
        public DateTime OldDate { get; set; }
        public double OldValue { get; set; }
        public string? OldNotes { get; set; }
        public int OldForeignKeyId { get; set; }
        public int BrancheId { get; set; }
        public int EditUserId { get; set; }
        public DateTime DateEdit { get; set; }
        public DateTime NewDate { get; set; }
        public double NewValue { get; set; }
        public string? NewNotes { get; set; }
        public int NewForeignKeyId { get; set; }
        public string Discriminator { get; set; } = null!;
        public DateTime? NewDueDate { get; set; }
        public DateTime? OldDueDate { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual User EditUser { get; set; } = null!;
    }
}
