using StoreManage.Server.Servicies.Interfacies.CustomerInterfacies;
using StoreManage.Server.Servicies.Interfacies.OrderInterfacies;
using StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies;
using StoreManage.Server.Servicies.Interfacies.SellerInterfacies;
using StoreManage.Server.Servicies.Interfacies.StatisticIntergacies;
using StoreManage.Server.Servicies.Interfacies.UserInterfacies;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {

        IBaseRepository<Catogry> Catogry { get; }
        IBaseRepository<Branche> Branche { get; }
        IBaseRepository<Product> Product { get; }
        IBaseRepository<ProductImage> ProductImage { get; }
        IBaseRepository<CustomerAddingSettlement> CustomerAddingSettlement { get; }
        IBaseRepository<CustomerDiscountSettlement> CustomerDiscountSettlement { get; }
        ICustomerRepository Customer { get; }
        IBaseRepository<CustomerType> CustomerType { get; }
        ISellerRepository Seller { get; }
        IBaseRepository<SellerAddingSettlement> SellerAddingSettlement { get; }
        IBaseRepository<SellerDiscountSettlement> SellerDiscountSettlement { get; }
        IBaseRepository<BankAccount> BankAccount { get; }



        IUserRepository User { get; }
        IOrderRepository Order { get; }
        IOrderBackRepository OrderBack { get; }
        IPurchaseRepository Purchase { get; }
        IPurchaseBackRepository PurchaseBack { get; }


        #region cash in

        IBaseRepository<CashInFromBankAccount> CashInFromBankAccount { get; }
        IBaseRepository<CashInFromBrancheMoneySafe> CashInFromBrancheMoneySafe { get; }
        IBaseRepository<CashInFromCustomer> CashInFromCustomer { get; }
        IBaseRepository<CashInFromIncome> CashInFromIncome { get; }
        IBaseRepository<CashInFromMasterMoneySafe> CashInFromMasterMoneySafe { get; }

        #endregion
        #region cash out

        IBaseRepository<CashOutToAdvancepaymentOfSalary> CashOutToAdvancepaymentOfSalary { get; }
        IBaseRepository<CashOutToBankAccount> CashOutToBankAccount { get; }
        IBaseRepository<CashOutToBrancheMoneySafe> CashOutToBrancheMoneySafe { get; }
        IBaseRepository<CashOutToMasterMoneySafe> CashOutToMasterMoneySafe { get; }
        IBaseRepository<CashOutToOutGoing> CashOutToOutGoing { get; }
        IBaseRepository<CashOutToSalary> CashOutToSalary { get; }
        IBaseRepository<CashOutToSeller> CashOutToSeller { get; }

        #endregion
        IBaseRepository<CashDayClose> CashDayClose { get; }

        #region money safe
        IBaseRepository<MasterMoneySafe> MasterMoneySafe { get; }
        IBaseRepository<BrancheMoneySafe> BrancheMoneySafe { get; }

        #endregion
        IBaseRepository<OutGoing> OutGoing { get; }
        IBaseRepository<InCome> InCome { get; }

        #region employee
        IBaseRepository<Employee> Employee { get; }
        IBaseRepository<EmployeeIncrease> EmployeeIncrease { get; }
        IBaseRepository<EmployeeLess> EmployeeLess { get; }
        IBaseRepository<EmployeePenalty> EmployeePenalty { get; }
        IBaseRepository<EmployeeReward> EmployeeReward { get; }

        #endregion


        #region statistics
        IStatisticsRepository Statistics { get; }
        #endregion
        int Complete();
    }
}
