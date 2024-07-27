using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.EmployeeDtos
{
    public class EmployeeProcessDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }

        public  string? EmployeeName { get; set; } = null!;
        public  string? UserSullName { get; set; } = null!;
    }
}
