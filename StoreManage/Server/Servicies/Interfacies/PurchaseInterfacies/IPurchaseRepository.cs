using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Dtos.PurchaseDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>
    {
        public List<PurchaseDto> GetAllPurchases(int brancheId);
        public List<PurchaseDto> GetAllForDate(DateTime date, int brancheId);
        public List<PurchaseDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int brancheId);
        public PurchaseDto GetPurchase(int id);
    }
}
