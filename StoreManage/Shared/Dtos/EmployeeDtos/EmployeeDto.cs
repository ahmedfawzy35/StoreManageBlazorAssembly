using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.EmployeeDtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adress { get; set; }
        public double Salary { get; set; }
        public string? Phone { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int BrancheId { get; set; }

        public  string? BrancheName { get; set; } = null!;
    }
}
