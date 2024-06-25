using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Models;
using System.Linq.Expressions;

namespace StoreManage.Server.Servicies.Repositories
{
    public class OrderBackRepository : BaseRepository<OrderBack>, IOrderBackRepository
    {
        private readonly AppDbContext _mycontext;
        public OrderBackRepository(AppDbContext context) : base(context)
        {
            _mycontext = context;
        }

        public List<OrderBackDto> GetAllOrdersBack(int brancheId)
        {
            var orders = _mycontext.OrderBacks.Include(x => x.Customer).Where(x => x.BrancheId == brancheId).ToList();
            if (orders != null)
            {
                return (ToOrderBackDto(orders!));
            }
            else
            {
                return new List<OrderBackDto>();
            }

        }
        public List<OrderBackDto> GetAllForDate(DateTime date, int brancheId)
        {
            var orders = _mycontext.OrderBacks.Include(x => x.Customer).Where(x => x.BrancheId == brancheId && x.Date.Date == date.Date).ToList();
            if (orders != null)
            {
                return (ToOrderBackDto(orders!));
            }
            else
            {
                return new List<OrderBackDto>();
            }
        }
        public List<OrderBackDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int barncheId)

        {
            var orders = _mycontext.OrderBacks.Include(x => x.Customer).Where(x => x.BrancheId == barncheId && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo).ToList();
            if (orders != null)
            {
                return (ToOrderBackDto(orders!));
            }
            else
            {
                return new List<OrderBackDto>();
            }
        }
        public OrderBackDto GetOrderBack(int id)
        {
            try
            {

                var myorder = _mycontext.OrderBacks.Include(x => x.Customer).Where(x => x.Id == id).FirstOrDefault();
                if (myorder != null)
                {
                    var myOrderBackDto = new OrderBackDto();
                    myOrderBackDto.Id = myorder.Id;
                    myOrderBackDto.Date = myorder.Date;
                    myOrderBackDto.CustomerId = myorder.CustomerId;
                    myOrderBackDto.Total = myorder.Total;
                    myOrderBackDto.Paid = myorder.Paid;
                    myOrderBackDto.Discount = myorder.Discount;
                    myOrderBackDto.BrancheId = myorder.BrancheId;
                    myOrderBackDto.OrderNumber = myorder.OrderNumber;
                    myOrderBackDto.Notes = myorder.Notes;


                    myOrderBackDto.CustomerName = myorder.Customer.Name;
                    return myOrderBackDto;
                }
                return new OrderBackDto();
            }
            catch (Exception)
            {

                return new OrderBackDto();
            }
        }
        private List<OrderBackDto> ToOrderBackDto(List<OrderBack> data)
        {
            var myOrders = new List<OrderBackDto>();

            foreach (var myorder in data)
            {
                var myOrderBackDto = new OrderBackDto();
                myOrderBackDto.Id = myorder.Id;
                myOrderBackDto.Date = myorder.Date;
                myOrderBackDto.CustomerId = myorder.CustomerId;
                myOrderBackDto.Total = myorder.Total;
                myOrderBackDto.Paid = myorder.Paid;
                myOrderBackDto.Discount = myorder.Discount;
                myOrderBackDto.BrancheId = myorder.BrancheId;
                myOrderBackDto.OrderNumber = myorder.OrderNumber;
                myOrderBackDto.Notes = myorder.Notes;
                myOrderBackDto.CustomerName = myorder.Customer.Name;
                myOrders.Add(myOrderBackDto);
            }

            return myOrders;
        }
    }
}
