using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.CashDtos.CashDayCloseDtos
{
    public class CashBallanceValues
    {
        public double lastDayBallance { get; set; }
        public double CashInFromOrders { get; set; }
        public double CashInFromPurcaseBacks { get; set; }
        public double cashInFromBankAccount { get; set; }
        public double cashInFromCustomer { get; set; }
        public double cashInFromIncome { get; set; }
        public double cashInFromMasterMoneySafe { get; set; }
        public double cashInFromBrancheMoneySafe { get; set; }
        public double totalIncome => cashInFromBankAccount + CashInFromPurcaseBacks + CashInFromOrders + cashInFromCustomer + cashInFromIncome + cashInFromMasterMoneySafe + cashInFromBrancheMoneySafe;
        public double CashOutToOrderBacks { get; set; }
        public double CashOutToOrderPurchase { get; set; }
        public double cashOutToAdvancepaymentOfSalary { get; set; }
        public double cashOutToBankAccount { get; set; }
        public double cashOutToMasterMoneySafe { get; set; }
        public double cashOutToBrancheMoneySafe { get; set; }
        public double cashOutToOutGoing { get; set; }
        public double cashOutToSalary { get; set; }
        public double cashOutToSeller { get; set; }
        public double totlOutcome => CashOutToOrderBacks + CashOutToOrderPurchase + cashOutToAdvancepaymentOfSalary + cashOutToBankAccount + cashOutToMasterMoneySafe + cashOutToBrancheMoneySafe + cashOutToOutGoing + cashOutToSalary + cashOutToSeller;
        public double cashBallance => lastDayBallance + totalIncome - totlOutcome;
    }
}
