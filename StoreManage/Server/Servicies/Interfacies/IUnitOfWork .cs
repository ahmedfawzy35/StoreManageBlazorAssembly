using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {

        IBaseRepository<Catogry> Catogry { get; }
        ICustomerRepository Customer { get; }
        ISellerRepository Seller { get; }
        IUserRepository User { get; }
        IOrderRepository Order { get; }
        IOrderBackRepository OrderBack { get; }
        int Complete();
    }
}
