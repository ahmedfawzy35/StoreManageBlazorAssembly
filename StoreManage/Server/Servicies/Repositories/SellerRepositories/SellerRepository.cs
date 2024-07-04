using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies.SellerInterfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.SellerDato;
using StoreManage.Shared.Models;
using StoreManage.Shared.Utilitis;

namespace StoreManage.Server.Servicies.Repositories.SellerRepositories
{
    public class SellerRepository : BaseRepository<Seller>, ISellerRepository
    {
        private readonly AppDbContext _context;
        private BaseRepository<Purchase> _PurchaseRepository;
        private BaseRepository<PurchaseBack> _PurchaseBackRepository;
        private BaseRepository<CashOutToSeller> _cashOutToSellerRepository;
        private BaseRepository<SellerAddingSettlement> _sellerAddingSettlementRepository;
        private BaseRepository<SellerDiscountSettlement> _sellerDiscountSettlementRepository;
        public SellerRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _PurchaseRepository = new BaseRepository<Purchase>(context);
            _PurchaseBackRepository = new BaseRepository<PurchaseBack>(context);
            _cashOutToSellerRepository = new BaseRepository<CashOutToSeller>(context);
            _sellerAddingSettlementRepository = new BaseRepository<SellerAddingSettlement>(context);
            _sellerDiscountSettlementRepository = new BaseRepository<SellerDiscountSettlement>(context);
        }



        public SellerAddDto Add(SellerAddDto entity)
        {
            var sel = new Seller();
            sel.Name = entity.Name;
            sel.Adress = entity.Adress;
            sel.BrancheId = entity.BrancheId;
            sel.StartAccount = entity.StartAccount;

            _context.Sellers.Add(sel);
            return entity;



        }
        public SellerAddDto Edit(SellerAddDto entity)
        {
            var sel = GetById(entity.Id);
            if (sel == null)
            {
                return entity;
            }
            sel.Name = entity.Name;
            sel.Adress = entity.Adress;
            sel.BrancheId = entity.BrancheId;
            sel.StartAccount = entity.StartAccount;
            _context.Sellers.Update(sel);
            return entity;



        }

        public async Task<SellerAccountDto> GetSellerAccount(int id, DateTime dateFrom, DateTime dateTo, bool showCashOrders = false)
        {
            // Get customer
            var seller = GetById(id);
            if (seller == null) return new SellerAccountDto();

            // get elements

            //1- get orders
            var purchases = await _PurchaseRepository.FindAllAsync(o => o.SellerId == seller.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //2- get order Back
            var purchaseBacks = await _PurchaseBackRepository.FindAllAsync(o => o.SellerId == seller.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //3- get cash In From Customer
            var cashOutToSeller = await _cashOutToSellerRepository.FindAllAsync(o => o.SellerId == seller.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //4- get customer AddingSettlements
            var sellerAddingSettlements = await _sellerAddingSettlementRepository.FindAllAsync(o => o.SellerId == seller.Id && o.Date >= dateFrom && o.Date <= dateTo);

            //5- getcustomerDiscountSettlements
            var sellerDiscountSettlements = await _sellerDiscountSettlementRepository.FindAllAsync(o => o.SellerId == seller.Id && o.Date >= dateFrom && o.Date <= dateTo);

            double LastAccount = seller.StartAccount
                                     + purchases.Where(x => x.IsDeleted == false && x.Date < dateFrom.Date).ToList().Sum(r => r.RemainingAmount)
                                     + sellerAddingSettlements.Where(x => x.Date < dateFrom.Date).ToList().Sum(r => r.Value)
                                     - purchaseBacks.Where(x => x.IsDeleted == false && x.Date < dateFrom.Date).ToList().Sum(r => r.RemainingAmount)
                                     - cashOutToSeller.Where(x => x.IsDeleted == false && x.IsDeleted == false && x.Date < dateFrom.Date).ToList().Sum(r => r.Value)
                                     - sellerDiscountSettlements.Where(x => x.Date < dateFrom.Date).ToList().Sum(r => r.Value);

            double TimeAccount = purchases.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.RemainingAmount)
                                                   + sellerAddingSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.Value)
                                                   - purchaseBacks.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.RemainingAmount)
                                                   - cashOutToSeller.Where(x => x.IsDeleted == false && x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.Value)
                                                   - sellerDiscountSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList().Sum(r => r.Value);

            //create elements list
            List<CustomerAccountElementDto> elements = new List<CustomerAccountElementDto>();
            var sellerrAccount = new SellerAccountDto();

            sellerrAccount.LastAccount = LastAccount;
            sellerrAccount.TimeAccount = TimeAccount;
            sellerrAccount.FinalTimeAccount = LastAccount + TimeAccount;
            sellerrAccount.FinalCustomerAccount = LastAccount + TimeAccount;

            sellerrAccount.FinalCustomerAccount = seller.SellrAccount;

            // set elements values 
            foreach (var element in purchases.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                if (!showCashOrders)
                    if (element.RemainingAmount <= 0) continue;

                elements.Add(new CustomerAccountElementDto { Id = element.Id, Number = element.OrderNumber, Value = element.RemainingAmount, Notes = "(فاتورة) " + element.Notes, Date = element.Date, Type = MyTypes.SellerAccountElementTyps.Purchase.ToString(), Add = true });

            }
            foreach (var element in purchaseBacks.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                if (!showCashOrders)
                    if (element.RemainingAmount <= 0) continue;
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Number = element.OrderNumber, Value = element.RemainingAmount, Notes = "(مرتجع) " + element.Notes, Date = element.Date, Type = MyTypes.SellerAccountElementTyps.PurchaseBack.ToString(), Add = false });
            }
            foreach (var element in cashOutToSeller.Where(x => x.IsDeleted == false && x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Value = element.Value, Notes = "(دفعة) " + element.Notes, Date = element.Date, Type = MyTypes.SellerAccountElementTyps.CashOutToSeller.ToString(), Add = false });
            }
            foreach (var element in sellerAddingSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Value = element.Value, Notes = "(تسوية اضافه) " + element.Notes, Date = element.Date, Type = MyTypes.SellerAccountElementTyps.SellerAddingSettlement.ToString(), Add = true });
            }
            foreach (var element in sellerDiscountSettlements.Where(x => x.Date >= dateFrom.Date && x.Date <= dateTo.Date).ToList())
            {
                elements.Add(new CustomerAccountElementDto { Id = element.Id, Value = element.Value, Notes = "(تسوية خصم) " + element.Notes, Date = element.Date, Type = MyTypes.SellerAccountElementTyps.SellerDiscountSettlement.ToString(), Add = false });
            }

            var sortedElements = elements.OrderBy(x => x.Date).ToList();

            for (int i = 0; i < sortedElements.Count; i++)
            {
                if (i == 0)
                {
                    sortedElements[i].AccountBeforElement = LastAccount;

                }
                else
                {
                    sortedElements[i].AccountBeforElement = sortedElements[i - 1].AccountAfterElement;
                }
            }

            // set customer account valus
            sellerrAccount.elements = sortedElements;
            sellerrAccount.SellerId = seller.Id;
            sellerrAccount.Name = seller.Name;
            return sellerrAccount;
        }
    }
}
