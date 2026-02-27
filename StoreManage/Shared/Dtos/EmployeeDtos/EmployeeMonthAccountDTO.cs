using StoreManage.Shared.Dtos.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.EmployeeDtos
{
    public class EmployeeMonthAccountDTO
    {
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public double Salary { get; set; }
        public List<EmployeeMonthAccountItemDTO>? Items { get; set; }
        public double TotalAdds => Items == null ? 0 : Items.Where(x => x.iSAdd).Sum(x => x.Value);
        public double TotalDebits => Items == null ? 0 : Items.Where(x => !x.iSAdd).Sum(x => x.Value);
        public double FinalSalary => Salary + TotalAdds - TotalDebits;
    }


    public class EmployeeMonthAccountItemDTO
    {
        public int IdItem { get; set; }
        public int EmployeeId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public bool iSAdd { get; set; }
        [Required(ErrorMessage = "النوع مطلوب")]
        public string? Type { get; set; }

    }
}
