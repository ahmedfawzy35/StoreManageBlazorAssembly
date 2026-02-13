using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.OrderInterfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Dtos.StatisticsDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.OrderRepositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _mycontext;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _mycontext = context;
        }
        public async Task<DayStatisticDto> GetDaySimpleStatisticForBranche(DayDto day)
        {

            DayStatisticDto dayStatisticDto = new DayStatisticDto();
            dayStatisticDto.Day = day.Date.Date;
            var orders = await _mycontext.Orders
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total);

            var purchases = await _mycontext.Purchases
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total - x.Discount);

            var ordersBack = await _mycontext.OrderBacks
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total - x.Discount);

            var purchasesBack = await _mycontext.PurchaseBacks
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total - x.Discount);

            var cashOrders = await _mycontext.Orders
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId && o.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var cashPurchases = await _mycontext.Purchases
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId && p.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var cashOrdersBack = await _mycontext.OrderBacks
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId && o.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var cashPurchasesBack = await _mycontext.PurchaseBacks
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId && p.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var productCountSoled = await _mycontext.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.Date.Date == day.Date.Date && od.Order.BrancheId == day.BrancheId)
                .SumAsync(od => od.Qte);

            var productCostSoled = await _mycontext.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .Where(od => od.Order.Date.Date == day.Date.Date && od.Order.BrancheId == day.BrancheId && od.Product != null)
                .SumAsync(od => (double)od.Qte * od.Product.LastPurchasePrice);

            var cashInFromBankAccount = await _mycontext.CashInFromBankAccounts
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromBrancheMoneySafe = await _mycontext.CashInFromBrancheMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromCustomer = await _mycontext.CashInFromCustomers
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromIncome = await _mycontext.CashInFromIncomes
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromMasterMoneySafe = await _mycontext.CashInFromMasterMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToAdvancepaymentOfSalary = await _mycontext.CashOutToAdvancepaymentOfSalaries
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToBankAccount = await _mycontext.CashOutToBankAccounts
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToBrancheMoneySafe = await _mycontext.CashOutToBrancheMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToMasterMoneySafe = await _mycontext.CashOutToMasterMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToOutGoing = await _mycontext.CashOutToOutGoings
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToSalary = await _mycontext.CashOutToSalaries
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToSeller = await _mycontext.CashOutToSellers
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            // Assign computed values to DTO (property names assumed; adjust if DTO properties differ)
           // dayStatisticDto.Day = day.Date;
            dayStatisticDto.TotalOrders = orders;
            dayStatisticDto.TotalPurchases = purchases;
            dayStatisticDto.TotalOrdersBack = ordersBack;
            dayStatisticDto.TotalPurchasesBack = purchasesBack;
            dayStatisticDto.CashOrders = cashOrders;
            dayStatisticDto.CashPurchases = cashPurchases;
            dayStatisticDto.CashOrdersBack = cashOrdersBack;
            dayStatisticDto.CashPurchasesBack = cashPurchasesBack;
            dayStatisticDto.ProductCountSoled = productCountSoled;
            dayStatisticDto.ProductCostSoled = productCostSoled;

            dayStatisticDto.CashInFromBankAccount = cashInFromBankAccount;
            dayStatisticDto.CashInFromBrancheMoneySafe = cashInFromBrancheMoneySafe;
            dayStatisticDto.CashInFromCustomer = cashInFromCustomer;
            dayStatisticDto.CashInFromIncome = cashInFromIncome;
            dayStatisticDto.CashInFromMasterMoneySafe = cashInFromMasterMoneySafe;

            dayStatisticDto.CashOutToAdvancepaymentOfSalary = cashOutToAdvancepaymentOfSalary;
            dayStatisticDto.CashOutToBankAccount = cashOutToBankAccount;
            dayStatisticDto.CashOutToBrancheMoneySafe = cashOutToBrancheMoneySafe;
            dayStatisticDto.CashOutToMasterMoneySafe = cashOutToMasterMoneySafe;
            dayStatisticDto.CashOutToOutGoing = cashOutToOutGoing;
            dayStatisticDto.CashOutToSalary = cashOutToSalary;
            dayStatisticDto.CashOutToSeller = cashOutToSeller;

            return dayStatisticDto;
        }
        public List<OrderDto> GetAllOrders(int brancheId)
        {
            var orders = _mycontext.Orders.Include(x => x.Customer).Where(x => x.BrancheId == brancheId).ToList();
            if (orders != null)
            {
                return ToOrderDto(orders!);
            }
            else
            {
                return new List<OrderDto>();
            }

        }
        public List<OrderDto> GetAllForDate(DateTime date, int brancheId)
        {
            var orders = _mycontext.Orders.Include(x => x.Customer).Where(x => x.BrancheId == brancheId && x.Date.Date == date.Date).ToList();
            if (orders != null)
            {
                return ToOrderDto(orders!);
            }
            else
            {
                return new List<OrderDto>();
            }
        }
        public List<OrderDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int barncheId)

        {
            var orders = _mycontext.Orders.Include(x => x.Customer).Where(x => x.BrancheId == barncheId && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo).ToList();
            if (orders != null)
            {
                return ToOrderDto(orders!);
            }
            else
            {
                return new List<OrderDto>();
            }
        }
        public OrderDto GetOrder(int id)
        {
            try
            {

                var myorder = _mycontext.Orders.Include(x => x.Customer).Where(x => x.Id == id).FirstOrDefault();
                if (myorder != null)
                {
                    var myorderDto = new OrderDto();
                    myorderDto.Id = myorder.Id;
                    myorderDto.Date = myorder.Date;
                    myorderDto.CustomerId = myorder.CustomerId;
                    myorderDto.Total = myorder.Total;
                    myorderDto.Paid = myorder.Paid;
                    myorderDto.Discount = myorder.Discount;
                    myorderDto.BrancheId = myorder.BrancheId;
                    myorderDto.OrderProfit = myorder.OrderProfit;
                    myorderDto.OrderNumber = myorder.OrderNumber;
                    myorderDto.Notes = myorder.Notes;


                    myorderDto.CustomerName = myorder.Customer.Name;
                    return myorderDto;
                }
                return new OrderDto();
            }
            catch (Exception)
            {

                return new OrderDto();
            }
        }
        private List<OrderDto> ToOrderDto(List<Order> data)
        {
            var myOrders = new List<OrderDto>();

            foreach (var myorder in data)
            {
                var myorderDto = new OrderDto();
                myorderDto.Id = myorder.Id;
                myorderDto.Date = myorder.Date;
                myorderDto.CustomerId = myorder.CustomerId;
                myorderDto.Total = myorder.Total;
                myorderDto.Paid = myorder.Paid;
                myorderDto.Discount = myorder.Discount;
                myorderDto.BrancheId = myorder.BrancheId;
                myorderDto.OrderProfit = myorder.OrderProfit;
                myorderDto.OrderNumber = myorder.OrderNumber;
                myorderDto.Notes = myorder.Notes;
                myorderDto.CustomerName = myorder.Customer.Name;
                myOrders.Add(myorderDto);
            }

            return myOrders;
        }
    }
}
