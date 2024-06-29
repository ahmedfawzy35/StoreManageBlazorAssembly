using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.OrderInterfacies;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.OrderRepositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _mycontext;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _mycontext = context;
        }

        public List<OrderDto> GetAllOrders(int brancheId)
        {
            var orders = _mycontext.Orders.Include(x => x.Customer).Where(x => x.BrancheId == brancheId).ToList();
            if (orders != null)
            {
                return ToOrderDto(orders!);
            }
            else
            {
                return new List<OrderDto>();
            }

        }
        public List<OrderDto> GetAllForDate(DateTime date, int brancheId)
        {
            var orders = _mycontext.Orders.Include(x => x.Customer).Where(x => x.BrancheId == brancheId && x.Date.Date == date.Date).ToList();
            if (orders != null)
            {
                return ToOrderDto(orders!);
            }
            else
            {
                return new List<OrderDto>();
            }
        }
        public List<OrderDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int barncheId)

        {
            var orders = _mycontext.Orders.Include(x => x.Customer).Where(x => x.BrancheId == barncheId && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo).ToList();
            if (orders != null)
            {
                return ToOrderDto(orders!);
            }
            else
            {
                return new List<OrderDto>();
            }
        }
        public OrderDto GetOrder(int id)
        {
            try
            {

                var myorder = _mycontext.Orders.Include(x => x.Customer).Where(x => x.Id == id).FirstOrDefault();
                if (myorder != null)
                {
                    var myorderDto = new OrderDto();
                    myorderDto.Id = myorder.Id;
                    myorderDto.Date = myorder.Date;
                    myorderDto.CustomerId = myorder.CustomerId;
                    myorderDto.Total = myorder.Total;
                    myorderDto.Paid = myorder.Paid;
                    myorderDto.Discount = myorder.Discount;
                    myorderDto.BrancheId = myorder.BrancheId;
                    myorderDto.OrderProfit = myorder.OrderProfit;
                    myorderDto.OrderNumber = myorder.OrderNumber;
                    myorderDto.Notes = myorder.Notes;


                    myorderDto.CustomerName = myorder.Customer.Name;
                    return myorderDto;
                }
                return new OrderDto();
            }
            catch (Exception)
            {

                return new OrderDto();
            }
        }
        private List<OrderDto> ToOrderDto(List<Order> data)
        {
            var myOrders = new List<OrderDto>();

            foreach (var myorder in data)
            {
                var myorderDto = new OrderDto();
                myorderDto.Id = myorder.Id;
                myorderDto.Date = myorder.Date;
                myorderDto.CustomerId = myorder.CustomerId;
                myorderDto.Total = myorder.Total;
                myorderDto.Paid = myorder.Paid;
                myorderDto.Discount = myorder.Discount;
                myorderDto.BrancheId = myorder.BrancheId;
                myorderDto.OrderProfit = myorder.OrderProfit;
                myorderDto.OrderNumber = myorder.OrderNumber;
                myorderDto.Notes = myorder.Notes;
                myorderDto.CustomerName = myorder.Customer.Name;
                myOrders.Add(myorderDto);
            }

            return myOrders;
        }
    }
}
