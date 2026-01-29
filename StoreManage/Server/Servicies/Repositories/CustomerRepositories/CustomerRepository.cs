using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreManage.Client.Pages.CustomerPages;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.CustomerInterfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;
using StoreManage.Shared.Utilitis;
using System.Data;

namespace StoreManage.Server.Servicies.Repositories.CustomerRepositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _mycontext;
        private BaseRepository<Order> _orderRepository;
        private BaseRepository<OrderBack> _orderBackRepository;
        private BaseRepository<CashInFromCustomer> _cashInFromCustomerRepository;
        private BaseRepository<CustomerAddingSettlement> _customerAddingSettlementRepository;
        private BaseRepository<CustomerDiscountSettlement> _customerDiscountSettlementRepository;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _mycontext = context;
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
            _mycontext.Customers.Add(cus);
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
            _mycontext.Customers.Update(cus);
            return entity;



        }
        public async Task<List<CustomersOrdersDto>> GetAllCustomersOrderAsync(int brancheId , DateTime dateFrom , DateTime dateTo)
        {
            // جلب البيانات من ال DbContext (صححنا الـ & إلى &&)
            // استبعاد العميل الافتراضي "عميل نقدي" مباشرة عن طريق الاسم
            var customers = await _context.Customers
                .Where(x => x.BrancheId == brancheId && x.Name != "عميل نقدي" && !x.Archived)
                .ToListAsync();


            // جلب الطلبيات مع استبعاد "عميل نقدي" وحساب الفترة
            var allOrders = await _context.Orders
                .Include(o => o.Customer)
                .Where(x => x.BrancheId == brancheId
                            && x.Customer.Name != "عميل نقدي"
                            && !x.Customer.Archived
                            && x.Date.Date >= dateFrom.Date
                            && x.Date.Date <= dateTo.Date)
                .ToListAsync();

            var allOrdersBack = await _context.OrderBacks
                .Include(o => o.Customer)
                .Where(x => x.BrancheId == brancheId
                            && x.Customer.Name != "عميل نقدي"
                            && !x.Customer.Archived
                            && x.Date.Date >= dateFrom.Date
                            && x.Date.Date <= dateTo.Date)
                .ToListAsync();

            customers = customers.OrderByDescending(c => c.CustomerAccount).ToList();
            var result = new List<CustomersOrdersDto>();
            int count = 1;
            foreach (var element in customers)
            {
                var totalOrders = allOrders.Where(x => x.CustomerId == element.Id && !x.IsDeleted == true && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo.Date).Sum(o => o.Total);
                var ordersCash = allOrders.Where(x => x.CustomerId == element.Id && !x.IsDeleted == true && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo.Date).Sum(o => o.Paid);
                var ordersUnCash = allOrders.Where(x => x.CustomerId == element.Id && !x.IsDeleted == true && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo.Date).Sum(o => o.RemainingAmount);
                var orderBacks = allOrdersBack.Where(x => x.CustomerId == element.Id && !x.IsDeleted == true && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo.Date).Sum(o => o.Total);
                var orders = totalOrders - orderBacks;

                

                result.Add(new CustomersOrdersDto
                {
                    CustomerId = element.Id,
                    Number = count++,
                    Name = element.Name,
                    TotalCashOrders = ordersCash,
                    TotalUnCashOrders = ordersUnCash,
                    TotalOrdersBack = orderBacks
                });
              
            }
            var orderedRows = result.OrderByDescending(r => r.FinalOrders).ToList();
           

            return orderedRows;
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
