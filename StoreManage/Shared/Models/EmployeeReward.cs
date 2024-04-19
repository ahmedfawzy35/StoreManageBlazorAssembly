using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class EmployeeReward
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
