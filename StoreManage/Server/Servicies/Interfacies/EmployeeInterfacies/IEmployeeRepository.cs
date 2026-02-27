using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.EmployeeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.EmployeeInterfacies
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public Task<EmployeeMonthAccountDTO> GetCustomerMonthAccount(int idEmployee, int month, int year);
    }
}
