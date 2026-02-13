using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Dtos.StatisticsDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.OrderInterfacies
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

        public List<OrderDto> GetAllOrders(int brancheId);
        public List<OrderDto> GetAllForDate(DateTime date, int brancheId);
        public List<OrderDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int brancheId);
        public OrderDto GetOrder(int id);
        public Task<DayStatisticDto> GetDaySimpleStatisticForBranche(DayDto day);
    }
}
