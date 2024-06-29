using StoreManage.Server.Servicies.Interfacies.CustomerInterfacies;
using StoreManage.Server.Servicies.Interfacies.OrderInterfacies;
using StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies;
using StoreManage.Server.Servicies.Interfacies.SellerInterfacies;
using StoreManage.Server.Servicies.Interfacies.UserInterfacies;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {

        IBaseRepository<Catogry> Catogry { get; }
        IBaseRepository<CustomerAddingSettlement> CustomerAddingSettlement { get; }
        IBaseRepository<CustomerDiscountSettlement> CustomerDiscountSettlement { get; }
        ICustomerRepository Customer { get; }
        ISellerRepository Seller { get; }
        IUserRepository User { get; }
        IOrderRepository Order { get; }
        IOrderBackRepository OrderBack { get; }
        IPurchaseRepository Purchase { get; }
        IPurchaseBackRepository PurchaseBack { get; }
        int Complete();
    }
}
