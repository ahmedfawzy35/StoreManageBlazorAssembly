using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CashDayCloses = new HashSet<CashDayClose>();
            CashOutToAdvancepaymentOfSalaries = new HashSet<CashOutToAdvancepaymentOfSalary>();
            CashOutToSalaries = new HashSet<CashOutToSalary>();
            EmployeeIncreases = new HashSet<EmployeeIncrease>();
            EmployeeLesses = new HashSet<EmployeeLess>();
            EmployeePenalties = new HashSet<EmployeePenalty>();
            EmployeeRewards = new HashSet<EmployeeReward>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adress { get; set; }
        public double Salary { get; set; }
        public string? Phone { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int BrancheId { get; set; }

        public virtual Branche Branche { get; set; } = null!;
        public virtual ICollection<CashDayClose> CashDayCloses { get; set; }
        public virtual ICollection<CashOutToAdvancepaymentOfSalary> CashOutToAdvancepaymentOfSalaries { get; set; }
        public virtual ICollection<CashOutToSalary> CashOutToSalaries { get; set; }
        public virtual ICollection<EmployeeIncrease> EmployeeIncreases { get; set; }
        public virtual ICollection<EmployeeLess> EmployeeLesses { get; set; }
        public virtual ICollection<EmployeePenalty> EmployeePenalties { get; set; }
        public virtual ICollection<EmployeeReward> EmployeeRewards { get; set; }
    }
}
