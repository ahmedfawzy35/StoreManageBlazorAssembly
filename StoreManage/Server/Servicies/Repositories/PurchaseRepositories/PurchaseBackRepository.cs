using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies;
using StoreManage.Shared.Dtos.PurchaseDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.PurchaseRepositories
{
    public class PurchaseBackRepository : BaseRepository<PurchaseBack>, IPurchaseBackRepository
    {
        private readonly AppDbContext _mycontext;
        public PurchaseBackRepository(AppDbContext context) : base(context)
        {
            _mycontext = context;
        }

        public List<PurchaseBackDto> GetAllPurchasesBack(int brancheId)
        {
            var PurchasesBack = _mycontext.PurchaseBacks.Include(x => x.Seller).Where(x => x.BrancheId == brancheId).ToList();
            if (PurchasesBack != null)
            {
                return ToPurchaseBackDto(PurchasesBack!);
            }
            else
            {
                return new List<PurchaseBackDto>();
            }

        }
        public List<PurchaseBackDto> GetAllForDate(DateTime date, int brancheId)
        {
            var PurchasesBack = _mycontext.PurchaseBacks.Include(x => x.Seller).Where(x => x.BrancheId == brancheId && x.Date.Date == date.Date).ToList();
            if (PurchasesBack != null)
            {
                return ToPurchaseBackDto(PurchasesBack!);
            }
            else
            {
                return new List<PurchaseBackDto>();
            }
        }
        public List<PurchaseBackDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int barncheId)

        {
            var purchasesBack = _mycontext.PurchaseBacks.Include(x => x.Seller).Where(x => x.BrancheId == barncheId && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo).ToList();
            if (purchasesBack != null)
            {
                return ToPurchaseBackDto(purchasesBack!);
            }
            else
            {
                return new List<PurchaseBackDto>();
            }
        }
        public PurchaseBackDto GetPurchaseBack(int id)
        {
            try
            {

                var PurchaseBack = _mycontext.PurchaseBacks.Include(x => x.Seller).Where(x => x.Id == id).FirstOrDefault();
                if (PurchaseBack != null)
                {
                    var PurchaseBackDto = new PurchaseBackDto();
                    PurchaseBackDto.Id = PurchaseBack.Id;
                    PurchaseBackDto.Date = PurchaseBack.Date;
                    PurchaseBackDto.SellerId = PurchaseBack.SellerId;
                    PurchaseBackDto.Total = PurchaseBack.Total;
                    PurchaseBackDto.Paid = PurchaseBack.Paid;
                    PurchaseBackDto.Discount = PurchaseBack.Discount;
                    PurchaseBackDto.BrancheId = PurchaseBack.BrancheId;
                    PurchaseBackDto.OrderNumber = PurchaseBack.OrderNumber;
                    PurchaseBackDto.Notes = PurchaseBack.Notes;


                    PurchaseBackDto.SellerName = PurchaseBack.Seller.Name;
                    return PurchaseBackDto;
                }
                return new PurchaseBackDto();
            }
            catch (Exception)
            {

                return new PurchaseBackDto();
            }
        }
        private List<PurchaseBackDto> ToPurchaseBackDto(List<PurchaseBack> data)
        {
            var my_purchasesBack = new List<PurchaseBackDto>();

            foreach (PurchaseBack my_purchaseBack in data)
            {
                var my_purchaseBackDto = new PurchaseBackDto();

                my_purchaseBackDto.Id = my_purchaseBack.Id;
                my_purchaseBackDto.Date = my_purchaseBack.Date;
                my_purchaseBackDto.SellerId = my_purchaseBack.SellerId;
                my_purchaseBackDto.Total = my_purchaseBack.Total;
                my_purchaseBackDto.Paid = my_purchaseBack.Paid;
                my_purchaseBackDto.Discount = my_purchaseBack.Discount;
                my_purchaseBackDto.BrancheId = my_purchaseBack.BrancheId;
                my_purchaseBackDto.OrderNumber = my_purchaseBack.OrderNumber;
                my_purchaseBackDto.Notes = my_purchaseBack.Notes;
                my_purchaseBackDto.SellerName = my_purchaseBack.Seller.Name;

                my_purchasesBack.Add(my_purchaseBackDto);
            }

            return my_purchasesBack;
        }
    }
}
