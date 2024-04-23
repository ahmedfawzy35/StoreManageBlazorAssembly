using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        public CustomerAddDto Add(CustomerAddDto entity);
        public CustomerAddDto Edit(CustomerAddDto entity);

        public Task< CustomerAccountDto> GetCustomerAccount(int id, DateTime dateFrom, DateTime dateTo, bool showCashOrders = false);

    }
}
