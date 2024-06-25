using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

        public List<OrderDto> GetAllOrders();
        public OrderDto GetOrder(int id);
    }
}
