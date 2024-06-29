using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.CustomerInterfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;
using StoreManage.Shared.Utilitis;

namespace StoreManage.Server.Servicies.Repositories.CustomerRepositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        private BaseRepository<Order> _orderRepository;
        private BaseRepository<OrderBack> _orderBackRepository;
        private BaseRepository<CashInFromCustomer> _cashInFromCustomerRepository;
        private BaseRepository<CustomerAddingSettlement> _customerAddingSettlementRepository;
        private BaseRepository<CustomerDiscountSettlement> _customerDiscountSettlementRepository;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _orderRepository = new BaseRepository<Order>(context);
            _orderBackRepository = new BaseRepository<OrderBack>(context);
            _cashInFromCustomerRepository = new BaseRepository<CashInFromCustomer>(context);
            _customerAddingSettlementRepository = new BaseRepository<CustomerAddingSettlement>(context);
            _customerDiscountSettlementRepository = new BaseRepository<CustomerDiscountSettlement>(context);
        }



        public CustomerAddDto Add(CustomerAddDto entity)
        {
            var cus = new Customer();
            cus.Name = entity.Name;
            cus.Adress = entity.Adress;
            cus.BrancheId = entity.BrancheId;
            cus.StartAccount = entity.StartAccount;
            cus.CustomertypeId = entity.CustomertypeId;
            cus.StopDealing = entity.StopDealing;
            _context.Customers.Add(cus);
            return entity;



        }
        public CustomerAddDto Edit(CustomerAddDto entity)
        {
            var cus = GetById(entity.Id);
            if (cus == null)
            {
                return entity;
            }
            cus.Name = entity.Name;
            cus.Adress = entity.Adress;
            cus.BrancheId = entity.BrancheId;
            cus.StartAccount = entity.StartAccount;
            cus.CustomertypeId = entity.CustomertypeId;
            cus.StopDealing = entity.StopDealing;
            _context.Customers.Update(cus);
            return entity;



        }

        public async Task<CustomerAccountDto> GetCustomerAccount(int id, DateTime dateFrom, DateTime dateTo, bool showCashOrders = false)
        {
            // Get customer
            var customer = GetById(id);
            if (customer == null) return new CustomerAccountDto();

            // get elements

            //1- get orders
            var orders = await _orderRepository.FindAllAsync(o => o.CustomerId == customer.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //2- get order Back
            var ordersBack = await _orderBackRepository.FindAllAsync(o => o.CustomerId == customer.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //3- get cash In From Customer
            var cashInFromCustomer = await _cashInFromCustomerRepository.FindAllAsync(o => o.CustomerId == customer.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //4- get customer AddingSettlements
            var customerAddingSettlements = await _customerAddingSettlementRepository.FindAllAsync(o => o.CustomerId == customer.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //5- getcustomerDiscountSettlements
            var customerDiscountSettlements = await _customerDiscountSettlementRepository.FindAllAsync(o => o.CustomerId == customer.Id && o.Date >= dateFrom && o.Date <= dateTo);

            double LastAccount = customer.StartAccount
                                     + orders.Where(x => x.IsDeleted == false && x.Date < dateFrom.Date).ToList().Sum(r => r.RemainingAmount)
                                     + customerAddingSettlements.Where(x => x.Date < dateFrom.Date).ToList().Sum(r => r.Value)
                                     - ordersBack.Where(x => x.IsDeleted == false && x.Date < dateFrom.Date).ToList().Sum(r => r.RemainingAmount)
                                     - cashInFromCustomer.Where(x => x.IsDeleted == false && x.IsDeleted == false && x.Date < dateFrom.Date).ToList().Sum(r => r.Value)
                                     - customerDiscountSettlements.Where(x => x.Date < dateFrom.Date).ToList().Sum(r => r.Value);

            double TimeAccount = orders.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.RemainingAmount)
                                                   + customerAddingSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.Value)
                                                   - ordersBack.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.RemainingAmount)
                                                   - cashInFromCustomer.Where(x => x.IsDeleted == false && x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.Value)
                                                   - customerDiscountSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.Value);

            //create elements list
            List<CustomerAccountElementDto> elements = new List<CustomerAccountElementDto>();
            var customerAccount = new CustomerAccountDto();

            customerAccount.LastAccount = LastAccount;
            customerAccount.TimeAccount = TimeAccount;
            customerAccount.FinalTimeAccount = LastAccount + TimeAccount;
            customerAccount.FinalCustomerAccount = customer.CustomerAccount;
            // set elements values 
            foreach (var element in orders.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                if (!showCashOrders)
                    if (element.RemainingAmount <= 0) continue;

                elements.Add(new CustomerAccountElementDto { Id = element.Id, Number = element.OrderNumber, Value = element.RemainingAmount, Notes = "(فاتورة) " + element.Notes, Date = element.Date, Type = MyTypes.CustomerAccountElementTyps.Order.ToString(), Add = true });

            }
            foreach (var element in ordersBack.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                if (!showCashOrders)
                    if (element.RemainingAmount <= 0) continue;
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Number = element.OrderNumber, Value = element.RemainingAmount, Notes = "(مرتجع) " + element.Notes, Date = element.Date, Type = MyTypes.CustomerAccountElementTyps.OrderBack.ToString(), Add = false });
            }
            foreach (var element in cashInFromCustomer.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Value = element.Value, Notes = "(تحصيل) " + element.Notes, Date = element.Date, Type = MyTypes.CustomerAccountElementTyps.CashInFromCustomer.ToString(), Add = false });
            }
            foreach (var element in customerAddingSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Value = element.Value, Notes = "(تسوية اضافه) " + element.Notes, Date = element.Date, Type = MyTypes.CustomerAccountElementTyps.CustomerAddingSettlement.ToString(), Add = true });
            }
            foreach (var element in customerDiscountSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Value = element.Value, Notes = "(تسوية خصم) " + element.Notes, Date = element.Date, Type = MyTypes.CustomerAccountElementTyps.CustomerDiscountSettlement.ToString(), Add = false });
            }

            var sortedElements = elements.OrderBy(x => x.Date).ToList();

            for (int i = 0; i < sortedElements.Count; i++)
            {
                if (i == 0)
                {
                    sortedElements[i].AccountBeforElement = LastAccount;

                }
                else
                {
                    sortedElements[i].AccountBeforElement = sortedElements[i - 1].AccountAfterElement;
                }
            }

            // set customer account valus
            customerAccount.elements = sortedElements;
            customerAccount.CustomerId = customer.Id;
            customerAccount.Name = customer.Name;
            return customerAccount;
        }
    }
}
