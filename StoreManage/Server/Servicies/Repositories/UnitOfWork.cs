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
       
        public ICustomerRepository Customer { get; private set; }
        public IBaseRepository<CustomerType> CustomerType { get; private set; }
        public IBaseRepository<CustomerDiscountSettlement> CustomerDiscountSettlement { get; private set; }
        public IBaseRepository<CustomerAddingSettlement> CustomerAddingSettlement { get; private set; }
        public ISellerRepository Seller { get; private set; }

        public IUserRepository User { get; private set; }
        public IOrderRepository Order { get; private set; }
          public IOrderBackRepository OrderBack { get; private set; }
        public IPurchaseRepository Purchase { get; private set; }
        public IPurchaseBackRepository PurchaseBack { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Catogry = new BaseRepository<Catogry>(_context);
            
            Customer = new CustomerRepository(_context );
            CustomerType = new BaseRepository<CustomerType>(_context);
            CustomerAddingSettlement = new BaseRepository<CustomerAddingSettlement>(_context);
            CustomerDiscountSettlement = new BaseRepository<CustomerDiscountSettlement>(_context);

            Seller = new SellerRepository(_context );
            User = new UserRepository(_context );
            Order = new OrderRepository(_context );
            OrderBack = new OrderBackRepository(_context );
            Purchase = new PurchaseRepository(_context );
            PurchaseBack = new PurchaseBackRepository(_context );

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
