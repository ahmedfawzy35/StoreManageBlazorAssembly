using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer? GetById(int id)
        {
            return _context.Customers.Where(x => x.Id == id).FirstOrDefault();
        }

        public CustomerAccountDto GetCustomerAccount(int id, DateTime dateFrom, DateTime dateTo, bool showCashOrders = false)
        {
            throw new NotImplementedException();
        }

        public   List<CustomerSearchDto> SearchName(string name, int branchId)
        {

            var customers =  _context.Customers.Include(c => c.Customertype).Where(x => x.Name!.Contains(name) && x.BrancheId == branchId && x.StopDealing == false).ToList();
                

            return customers.Select(c => new CustomerSearchDto
            {
                Id = c.Id,
                Name = c.Name,
                Adress = c.Adress,
                Type = c.Customertype.Name
            }).ToList(); 
        }

        public void StartDealingCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void StopDealingCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Customer entity)
        {
            throw new NotImplementedException();
        }


    }
}
