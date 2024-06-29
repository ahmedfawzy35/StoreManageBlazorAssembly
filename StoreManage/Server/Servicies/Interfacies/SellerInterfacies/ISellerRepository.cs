using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.SellerDato;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Servicies.Interfacies.SellerInterfacies
{
    public interface ISellerRepository : IBaseRepository<Seller>
    {
        public SellerAddDto Add(SellerAddDto entity);
        public SellerAddDto Edit(SellerAddDto entity);

        public Task<SellerAccountDto> GetSellerAccount(int id, DateTime dateFrom, DateTime dateTo, bool showCashOrders = false);

    }
}
