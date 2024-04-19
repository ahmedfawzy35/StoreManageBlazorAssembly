using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface ICustomerRepository
    {
        void Add(Customer entity);
        void Update(int id, Customer entity);
        void Delete(int id);
        Customer? GetById(int id);
        List<Customer> GetAll();
        List<CustomerSearchDto> SearchName(string name , int branchId);
        CustomerAccountDto GetCustomerAccount(int id, DateTime dateFrom, DateTime dateTo, bool showCashOrders = false);

        void StopDealingCustomer(int id);
        void StartDealingCustomer(int id);
    }
}
