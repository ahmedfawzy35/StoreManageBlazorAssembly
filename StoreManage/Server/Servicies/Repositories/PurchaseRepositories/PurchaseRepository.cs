using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.OrderInterfacies;
using StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Dtos.PurchaseDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Repositories.PurchaseRepositories
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        private readonly AppDbContext _mycontext;
        public PurchaseRepository(AppDbContext context) : base(context)
        {
            _mycontext = context;
        }

        public List<PurchaseDto> GetAllPurchases(int brancheId)
        {
            var Purchases = _mycontext.Purchases.Include(x => x.Seller).Where(x => x.BrancheId == brancheId).ToList();
            if (Purchases != null)
            {
                return ToPurchaseDto(Purchases!);
            }
            else
            {
                return new List<PurchaseDto>();
            }

        }
        public List<PurchaseDto> GetAllForDate(DateTime date, int brancheId)
        {
            var Purchases = _mycontext.Purchases.Include(x => x.Seller).Where(x => x.BrancheId == brancheId && x.Date.Date == date.Date).ToList();
            if (Purchases != null)
            {
                return ToPurchaseDto(Purchases!);
            }
            else
            {
                return new List<PurchaseDto>();
            }
        }
        public List<PurchaseDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int barncheId)

        {
            var Purchases = _mycontext.Purchases.Include(x => x.Seller).Where(x => x.BrancheId == barncheId && x.Date.Date >= dateFrom.Date && x.Date.Date <= dateTo).ToList();
            if (Purchases != null)
            {
                return ToPurchaseDto(Purchases!);
            }
            else
            {
                return new List<PurchaseDto>();
            }
        }
        public PurchaseDto GetPurchase(int id)
        {
            try
            {

                var Purchase = _mycontext.Purchases.Include(x => x.Seller).Where(x => x.Id == id).FirstOrDefault();
                if (Purchase != null)
                {
                    var PurchaseDto = new PurchaseDto();
                    PurchaseDto.Id = Purchase.Id;
                    PurchaseDto.Date = Purchase.Date;
                    PurchaseDto.SellerId = Purchase.SellerId;
                    PurchaseDto.Total = Purchase.Total;
                    PurchaseDto.Paid = Purchase.Paid;
                    PurchaseDto.Discount = Purchase.Discount;
                    PurchaseDto.BrancheId = Purchase.BrancheId;
                    PurchaseDto.OrderNumber = Purchase.OrderNumber;
                    PurchaseDto.Notes = Purchase.Notes;


                    PurchaseDto.SellerName = Purchase.Seller.Name;
                    return PurchaseDto;
                }
                return new PurchaseDto();
            }
            catch (Exception)
            {

                return new PurchaseDto();
            }
        }
        private List<PurchaseDto> ToPurchaseDto(List<Purchase> data)
        {
            var my_purchases = new List<PurchaseDto>();

            foreach (Purchase my_purchase in data)
            {
                var my_purchaseDto = new PurchaseDto();
                my_purchaseDto.Id = my_purchase.Id;
                my_purchaseDto.Date = my_purchase.Date;
                my_purchaseDto.SellerId = my_purchase.SellerId;
                my_purchaseDto.Total = my_purchase.Total;
                my_purchaseDto.Paid = my_purchase.Paid;
                my_purchaseDto.Discount = my_purchase.Discount;
                my_purchaseDto.BrancheId = my_purchase.BrancheId;
                my_purchaseDto.OrderNumber = my_purchase.OrderNumber;
                my_purchaseDto.Notes = my_purchase.Notes;
                my_purchaseDto.SellerName = my_purchase.Seller.Name;
                my_purchases.Add(my_purchaseDto);
            }

            return my_purchases;
        }
    }
}
