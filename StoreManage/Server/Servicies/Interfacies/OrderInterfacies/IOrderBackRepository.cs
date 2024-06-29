using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.OrderInterfacies
{
    public interface IOrderBackRepository : IBaseRepository<OrderBack>
    {
        public List<OrderBackDto> GetAllOrdersBack(int brancheId);
        public List<OrderBackDto> GetAllForDate(DateTime date, int brancheId);
        public List<OrderBackDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int brancheId);
        public OrderBackDto GetOrderBack(int id);
    }
}
