using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Server.Servicies.Interfacies.CustomerInterfacies;
using StoreManage.Server.Servicies.Interfacies.OrderInterfacies;
using StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies;
using StoreManage.Server.Servicies.Interfacies.SellerInterfacies;
using StoreManage.Server.Servicies.Interfacies.UserInterfacies;
using StoreManage.Server.Servicies.Repositories.CustomerRepositories;
using StoreManage.Server.Servicies.Repositories.OrderRepositories;
using StoreManage.Server.Servicies.Repositories.PurchaseRepositories;
using StoreManage.Server.Servicies.Repositories.SellerRepositories;
using StoreManage.Server.Servicies.Repositories.UsererRepositories;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBaseRepository<Catogry> Catogry { get; private set; }
        public IBaseRepository<Branche> Branche { get; private set; }
        public IBaseRepository<BankAccount> BankAccount { get; private set; }
        public IBaseRepository<Product> Product { get; private set; }
       
        public ICustomerRepository Customer { get; private set; }
        public IBaseRepository<CustomerType> CustomerType { get; private set; }
        public IBaseRepository<CustomerDiscountSettlement> CustomerDiscountSettlement { get; private set; }
        public IBaseRepository<CustomerAddingSettlement> CustomerAddingSettlement { get; private set; }
        public ISellerRepository Seller { get; private set; }
        public IBaseRepository<SellerAddingSettlement> SellerAddingSettlement { get; private set; }
        public IBaseRepository<SellerDiscountSettlement> SellerDiscountSettlement { get; private set; }
        public IUserRepository User { get; private set; }
        public IOrderRepository Order { get; private set; }
          public IOrderBackRepository OrderBack { get; private set; }
        public IPurchaseRepository Purchase { get; private set; }
        public IPurchaseBackRepository PurchaseBack { get; private set; }

        public IBaseRepository<CashInFromBankAccount> CashInFromBankAccount  { get; private set; }

        public IBaseRepository<CashInFromBrancheMoneySafe> CashInFromBrancheMoneySafe  { get; private set; }

        public IBaseRepository<CashInFromCustomer> CashInFromCustomer  { get; private set; }

        public IBaseRepository<CashInFromIncome> CashInFromIncome  { get; private set; }

        public IBaseRepository<CashInFromMasterMoneySafe> CashInFromMasterMoneySafe  { get; private set; }

        public IBaseRepository<CashOutToAdvancepaymentOfSalary> CashOutToAdvancepaymentOfSalary  { get; private set; }

        public IBaseRepository<CashOutToBankAccount> CashOutToBankAccount  { get; private set; }

        public IBaseRepository<CashOutToBrancheMoneySafe> CashOutToBrancheMoneySafe  { get; private set; }

        public IBaseRepository<CashOutToMasterMoneySafe> CashOutToMasterMoneySafe  { get; private set; }

        public IBaseRepository<CashOutToOutGoing> CashOutToOutGoing  { get; private set; }

        public IBaseRepository<CashOutToSalary> CashOutToSalary  { get; private set; }

        public IBaseRepository<CashOutToSeller> CashOutToSeller  { get; private set; }

        public IBaseRepository<CashDayClose> CashDayClose  { get; private set; }

        public IBaseRepository<MasterMoneySafe> MasterMoneySafe  { get; private set; }

        public IBaseRepository<BrancheMoneySafe> BrancheMoneySafe  { get; private set; }

        public IBaseRepository<OutGoing> OutGoing  { get; private set; }

        public IBaseRepository<InCome> InCome  { get; private set; }

        public IBaseRepository<Employee> Employee  { get; private set; }

        public IBaseRepository<EmployeeIncrease> EmployeeIncrease  { get; private set; }

        public IBaseRepository<EmployeeLess> EmployeeLess  { get; private set; }

        public IBaseRepository<EmployeePenalty> EmployeePenalty  { get; private set; }

        public IBaseRepository<EmployeeReward> EmployeeReward  { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Catogry = new BaseRepository<Catogry>(_context);
            Branche = new BaseRepository<Branche>(_context);
            BankAccount = new BaseRepository<BankAccount>(_context);
            Product = new BaseRepository<Product>(_context);
            
            Customer = new CustomerRepository(_context );
            CustomerType = new BaseRepository<CustomerType>(_context);
            CustomerAddingSettlement = new BaseRepository<CustomerAddingSettlement>(_context);
            CustomerDiscountSettlement = new BaseRepository<CustomerDiscountSettlement>(_context);

            Seller = new SellerRepository(_context );
            SellerAddingSettlement = new BaseRepository<SellerAddingSettlement>(_context);
            SellerDiscountSettlement = new BaseRepository<SellerDiscountSettlement>(_context);

            User = new UserRepository(_context );
            Order = new OrderRepository(_context );
            OrderBack = new OrderBackRepository(_context );
            Purchase = new PurchaseRepository(_context );
            PurchaseBack = new PurchaseBackRepository(_context );

            CashInFromBankAccount = new BaseRepository<CashInFromBankAccount>(_context);

            CashInFromBrancheMoneySafe = new BaseRepository<CashInFromBrancheMoneySafe>(_context);

       CashInFromCustomer  = new BaseRepository<CashInFromCustomer>(_context);

            CashInFromIncome = new BaseRepository<CashInFromIncome>(_context);

            CashInFromMasterMoneySafe = new BaseRepository<CashInFromMasterMoneySafe>(_context);

            CashOutToAdvancepaymentOfSalary = new BaseRepository<CashOutToAdvancepaymentOfSalary>(_context);

            CashOutToBankAccount = new BaseRepository<CashOutToBankAccount>(_context);

            CashOutToBrancheMoneySafe = new BaseRepository<CashOutToBrancheMoneySafe>(_context);

       CashOutToMasterMoneySafe = new BaseRepository<CashOutToMasterMoneySafe>(_context);

      CashOutToOutGoing = new BaseRepository<CashOutToOutGoing>(_context);

        CashOutToSalary = new BaseRepository<CashOutToSalary>(_context);

        CashOutToSeller = new BaseRepository<CashOutToSeller>(_context);

        CashDayClose = new BaseRepository<CashDayClose>(_context);

      MasterMoneySafe = new BaseRepository<MasterMoneySafe>(_context);

         BrancheMoneySafe = new BaseRepository<BrancheMoneySafe>(_context);

         OutGoing = new BaseRepository<OutGoing>(_context);

        InCome = new BaseRepository<InCome>(_context);

         Employee = new BaseRepository<Employee>(_context);

         EmployeeIncrease = new BaseRepository<EmployeeIncrease>(_context);

         EmployeeLess = new BaseRepository<EmployeeLess>(_context);

        EmployeePenalty = new BaseRepository<EmployeePenalty>(_context);

        EmployeeReward = new BaseRepository<EmployeeReward>(_context);

    }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
