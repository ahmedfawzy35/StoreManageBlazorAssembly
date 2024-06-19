using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class CashDayClose
    {
        public int Id { get; set; }
        public DateTime Dte { get; set; }
        public double AccountValue { get; set; }
        public double RealAccountValue { get; set; }
        public double TotalInCome { get; set; }
        public double TotalOutCome { get; set; }
        public double LastDayAccount { get; set; }
        public double BudgetDefuciency { get; set; }
        public int UserId { get; set; }
        public int ResposibleEmployeeId { get; set; }
        public int BrancheId { get; set; }
        public DateTime DayCloseDate { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual Employee ResposibleEmployee { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
