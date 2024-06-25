using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBaseRepository<Catogry> Catogry { get; private set; }
        public ICustomerRepository Customer { get; private set; }

        public ISellerRepository Seller { get; private set; }

        public IUserRepository User { get; private set; }
        public IOrderRepository Order { get; private set; }
          public IOrderBackRepository OrderBack { get; private set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Catogry = new BaseRepository<Catogry>(_context);
            Customer = new CustomerRepository(_context );
            Seller = new SellerRepository(_context );
            User = new UserRepository(_context );
            Order = new OrderRepository(_context );
            OrderBack = new OrderBackRepository(_context );
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
