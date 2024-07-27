using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CashDtos.CashOutDtos
{
    public class CashOutToMasterMoneySafeDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string? Notes { get; set; }
        public int MasterMoneySafeId { get; set; }
        public int UserId { get; set; }
        public int BrancheId { get; set; }

        public string? BrancheName { get; set; } = null!;
        public string? MasterMoneySafeName { get; set; } = null!;
        public string? UserFullName { get; set; } = null!;
    }
}
