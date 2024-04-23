using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Utilitis
{
    public class MyTypes
    {
        public enum OrdersTyps
        {
            Order,
            OrderBack,
            Purchase,
            PurchaseBack
        }
        public enum CustomerAccountElementTyps
        {
            Order,
            OrderBack,
            CashInFromCustomer,
            CustomerAddingSettlement,
            CustomerDiscountSettlement,

        }
        public enum SellerAccountElementTyps
        {
            Purchase,
            PurchaseBack,
            CashOutToSeller,
            SellerAddingSettlement,
            SellerDiscountSettlement

        }
        public enum EmployeeSalaryElemntTypes
        {
            EmployeeLesses,
            EmployeeIncreases,
            CashOutToAdvancepaymentOfSalaries,
            CashOutToSalaries,
            EmployeePenalties,
            EmployeeReward,

        }
        public enum EmployeeProcessTypes
        {
            EmployeeLesses,
            EmployeeIncreases,
            AbsenceFromWork,
            EmployeePenalties,
            EmployeeReward,

        }
        public enum CashItemTyps
        {
            None = 0,
            Order,
            OrderBack,
            Purchase,
            PurshaseBack,
            cashInFromBankAccount,
            cashInFromCustomer,
            cashInFromIncome,
            cashInFromMasterMoneySafe,
            cashInFromBrancheMoneySafe,
            cashOutToAdvancepaymentOfSalary,
            cashOutToBankAccount,
            cashOutToMasterMoneySafe,
            cashOutToBrancheMoneySafe,
            cashOutToOutGoing,
            cashOutToSalary,
            cashOutToSeller,

        }
        public enum CashInTyps
        {
            None = 0,
            cashInFromBankAccount,
            cashInFromCustomer,
            cashInFromIncome,
            cashInFromMasterMoneySafe,
            cashInFromBrancheMoneySafe,

        }
        public enum CashOutTyps
        {
            None = 0,

            cashOutToAdvancepaymentOfSalary,
            cashOutToBankAccount,
            cashOutToMasterMoneySafe,
            cashOutToOutGoing,
            cashOutToSalary,
            cashOutToSeller,
            cashOutToBrancheMoneySafe,
        }

        public enum CashModelTyps
        {
            None = 0,
            cashInFromBankAccount,
            cashInFromCustomer,
            cashInFromIncome,
            cashInFromMasterMoneySafe,
            cashInFromBrancheMoneySafe,
            cashOutToAdvancepaymentOfSalary,
            cashOutToBankAccount,
            cashOutToMasterMoneySafe,
            cashOutToOutGoing,
            cashOutToSalary,
            cashOutToSeller,
            cashOutToBrancheMoneySafe,

        }


        public enum PermissionsType
        {
            None = 0,
            View,
            Create,
            Edit,
            Delete,

        }

    }
}
