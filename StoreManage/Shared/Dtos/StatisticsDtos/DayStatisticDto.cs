using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.StatisticsDtos
{
    public class DayStatisticDto
    {
        public DateTime Day { get; set; }
        public bool ManageStore { get; set; }
        public double TotalOrders { get; set; }
        public double CashOrders { get; set; }
        public double UnCashOrders => TotalOrders - CashOrders;

        public double TotalOrdersBack { get; set; }
        public double CashOrdersBack { get; set; }
        public double UnCashOrdersBack => TotalOrdersBack - CashOrdersBack;

        public double TotalPurchases { get; set; }
        public double CashPurchases { get; set; }
        public double UnCasPurchases => TotalPurchases - CashPurchases;

        public double TotalPurchasesBack { get; set; }
        public double CashPurchasesBack { get; set; }
        public double UnCasPurchasesBack => TotalPurchasesBack - CashPurchasesBack;

        public int ProductCountSoled { get; set; }
        public double ProductCostSoled { get; set; }

        public double CashInFromBankAccount { get; set; }
        public double CashInFromBrancheMoneySafe { get; set; }
        public double CashInFromCustomer { get; set; }
        public double CashInFromIncome { get; set; }
        public double CashInFromMasterMoneySafe { get; set; }

        public double CashOutToAdvancepaymentOfSalary { get; set; }
        public double CashOutToBankAccount { get; set; }
        public double CashOutToBrancheMoneySafe { get; set; }
        public double CashOutToMasterMoneySafe { get; set; }
        public double CashOutToOutGoing { get; set; }
        public double CashOutToSalary { get; set; }
        public double CashOutToSeller { get; set; }


        public double TotalIncomes => CashOrders+ CashPurchasesBack+ CashInFromBankAccount + CashInFromBrancheMoneySafe + CashInFromCustomer + CashInFromIncome + CashInFromMasterMoneySafe;
        public double TotaOutgoing => CashOrdersBack + CashPurchases + CashOutToAdvancepaymentOfSalary + CashOutToBankAccount + CashOutToBrancheMoneySafe + CashOutToMasterMoneySafe + CashOutToOutGoing + CashOutToSalary + CashOutToSeller;
        public double FinalDayCash => TotalIncomes - TotaOutgoing ;
        public double ExpectedProfit =>  ManageStore? TotalOrders - TotalOrdersBack - ProductCostSoled : (TotalOrders - TotalOrdersBack)*.10 ;
    }
}
