using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {

        IBaseRepository<Catogry> Catogry { get; }
        ICustomerRepository Customer { get; }
        int Complete();
    }
}
