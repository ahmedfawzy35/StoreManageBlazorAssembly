using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.StatisticIntergacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.StatisticsDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.StatisticRepositories
{
    public class StatisticRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DayStatisticDto> GetDaySimpleStatisticForBranche(DayDto day)
        {

            DayStatisticDto dayStatisticDto = new DayStatisticDto();
             dayStatisticDto.Day = day.Date;
            var orders = await _context.Orders
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total);

            var purchases = await _context.Purchases
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total - x.Discount);

            var ordersBack = await _context.OrderBacks
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total - x.Discount);

            var purchasesBack = await _context.PurchaseBacks
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId)
                .SumAsync(x => x.Total - x.Discount);

            var cashOrders = await _context.Orders
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId && o.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var cashPurchases = await _context.Purchases
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId && p.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var cashOrdersBack = await _context.OrderBacks
                .Where(o => o.Date.Date == day.Date.Date && o.BrancheId == day.BrancheId && o.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var cashPurchasesBack = await _context.PurchaseBacks
                .Where(p => p.Date.Date == day.Date.Date && p.BrancheId == day.BrancheId && p.RemainingAmount == 0)
                .SumAsync(x => x.Paid);

            var productCountSoled = await _context.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.Date.Date == day.Date.Date && od.Order.BrancheId == day.BrancheId)
                .SumAsync(od => od.Qte);

            var productCostSoled = await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .Where(od => od.Order.Date.Date == day.Date.Date && od.Order.BrancheId == day.BrancheId && od.Product != null)
                .SumAsync(od => (double)od.Qte * od.Product.LastPurchasePrice);

            var cashInFromBankAccount = await _context.CashInFromBankAccounts
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromBrancheMoneySafe = await _context.CashInFromBrancheMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromCustomer = await _context.CashInFromCustomers
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromIncome = await _context.CashInFromIncomes
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashInFromMasterMoneySafe = await _context.CashInFromMasterMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToAdvancepaymentOfSalary = await _context.CashOutToAdvancepaymentOfSalaries
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToBankAccount = await _context.CashOutToBankAccounts
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToBrancheMoneySafe = await _context.CashOutToBrancheMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToMasterMoneySafe = await _context.CashOutToMasterMoneySaves
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToOutGoing = await _context.CashOutToOutGoings
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToSalary = await _context.CashOutToSalaries
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            var cashOutToSeller = await _context.CashOutToSellers
                .Where(cf => cf.Date.Date == day.Date.Date && cf.BrancheId == day.BrancheId)
                .SumAsync(cf => cf.Value);

            // Assign computed values to DTO (property names assumed; adjust if DTO properties differ)
            dayStatisticDto.Day = day.Date;
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
    }

    public class ProductCostSoledclassDto
    {
        public int Qte { get; set; }
        public double Cost { get; set; }
    }
}