using StoreManage.Shared.Dtos.PurchaseDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.PurchaseInterfacies
{
    public interface IPurchaseBackRepository : IBaseRepository<PurchaseBack>
    {
        public List<PurchaseBackDto> GetAllPurchasesBack(int brancheId);
        public List<PurchaseBackDto> GetAllForDate(DateTime date, int brancheId);
        public List<PurchaseBackDto> GetAllForTime(DateTime dateFrom, DateTime dateTo, int brancheId);
        public PurchaseBackDto GetPurchaseBack(int id);
    }
}
