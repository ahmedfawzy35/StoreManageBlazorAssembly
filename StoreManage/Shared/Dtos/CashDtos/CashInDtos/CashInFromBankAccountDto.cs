using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CashDtos.CashInDtos
{
    public class CashInFromBankAccountDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string? Notes { get; set; }
        public int BanckAccountId { get; set; }
        public int UserId { get; set; }
        public int BrancheId { get; set; }

        public string? BrancheName { get; set; } = null!;
        public string? BanckAccountName { get; set; } = null!;
        public string? BanckAccountBrancheName { get; set; } = null!;

        public string? UserFullName { get; set; } = null!;
        
    }
}
