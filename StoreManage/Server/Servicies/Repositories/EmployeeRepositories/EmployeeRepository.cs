using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.CustomerInterfacies;
using StoreManage.Server.Servicies.Interfacies.EmployeeInterfacies;
using StoreManage.Shared.Dtos.EmployeeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
       
            private readonly AppDbContext _mycontext;

            public EmployeeRepository(AppDbContext mycontext) : base(mycontext)
            {
                _mycontext = mycontext;
        }


        public Task<EmployeeMonthAccountDTO> GetCustomerMonthAccount(int idEmployee, int month, int year)
        {

            var montAccount = new EmployeeMonthAccountDTO();

                var employee = _mycontext.Employees.FirstOrDefault(e => e.Id == idEmployee);
                if (employee == null)
                {
                    return Task.FromResult<EmployeeMonthAccountDTO>(null);
                }


            // جلب كل العمليات المالية المتعلقة بالموظف في الشهر المحدد
            var increases = _mycontext.EmployeeIncreases
                .Where(e => e.EmployeeId == idEmployee && e.Date.Month == month && e.Date.Year == year)
                .Select(e=> new EmployeeMonthAccountItemDTO
                {
                    Date = e.Date,
                    Notes = e.Notes,
                    Value = e.Value,
                    Type = "EmployeeIncreases",
                    EmployeeId = e.EmployeeId,
                    IdItem = e.Id,
                    UserId = e.UserId,
                    iSAdd = true

                }).ToList();

            var decreases = _mycontext.EmployeeLesses
                .Where(e => e.EmployeeId == idEmployee && e.Date.Month == month && e.Date.Year == year)
                .Select(e => new EmployeeMonthAccountItemDTO
                {
                    Date = e.Date,
                    Notes = e.Notes,
                    Value = e.Value,
                    Type = "EmployeeLesses",
                    EmployeeId = e.EmployeeId,
                    IdItem = e.Id,
                    UserId = e.UserId,
                    iSAdd = false
                }).ToList();

            var cashOutToAdvancepaymentOfSalaries = _mycontext.CashOutToAdvancepaymentOfSalaries
                .Where(e => e.EmployeeId == idEmployee && e.Date.Month == month && e.Date.Year == year)
                .Select(e => new EmployeeMonthAccountItemDTO
                {
                    Date = e.Date,
                    Notes = e.Notes,
                    Value = e.Value,
                    Type = "CashOutToAdvancepaymentOfSalaries",
                    EmployeeId = e.EmployeeId,
                    IdItem = e.Id,
                    UserId = e.UserId,
                    iSAdd = false
                }).ToList();

            var employeePenaltys = _mycontext.EmployeePenalties
                .Where(e => e.EmployeeId == idEmployee && e.Date.Month == month && e.Date.Year == year)
                .Select(e => new EmployeeMonthAccountItemDTO
                {
                    Date = e.Date,
                    Notes = e.Notes,
                    Value = e.Value,
                    Type = "EmployeePenalties",
                    EmployeeId = e.EmployeeId,
                    IdItem = e.Id,
                    UserId = e.UserId,
                    iSAdd = false
                }).ToList();

            var employeeRewards = _mycontext.EmployeeRewards
                .Where(e => e.EmployeeId == idEmployee && e.Date.Month == month && e.Date.Year == year)
                .Select(e => new EmployeeMonthAccountItemDTO
                {
                    Date = e.Date,
                    Notes = e.Notes,
                    Value = e.Value,
                    Type = "EmployeeRewards",
                    EmployeeId = e.EmployeeId,
                    IdItem = e.Id,
                    UserId = e.UserId,
                    iSAdd = true
                }).ToList();
            montAccount.EmployeeId = employee.Id;
            montAccount.Name = employee.Name;
            montAccount.Salary = employee.Salary;
           var Items = new List<EmployeeMonthAccountItemDTO>();
            foreach (var item in employeeRewards)
            {
                Items.Add(item);
            }
            foreach (var item in employeePenaltys)
            {
                Items.Add(item);
            }
            foreach (var item in cashOutToAdvancepaymentOfSalaries)
            {
               Items.Add(item);
            }
            foreach (var item in decreases)
            {
                Items.Add(item);
            }
            foreach (var item in increases)
            {
                Items.Add(item);
            }

            var sortedItems = Items.OrderBy(i => i.Date).ToList();
            montAccount.Items = sortedItems;
            montAccount.Items = sortedItems;

            return Task.FromResult(montAccount);
        }

    }
}
