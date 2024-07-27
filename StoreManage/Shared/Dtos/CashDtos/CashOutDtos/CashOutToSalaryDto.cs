using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CashDtos.CashOutDtos
{
    public class CashOutToSalaryDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime ProcessDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int BrancheId { get; set; }
        

        public string?  BrancheName { get; set; } = null!;
        public string? EmployeeName { get; set; } = null!;
        public string? UserFullName { get; set; } = null!;
    }
}
