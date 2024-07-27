using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.CashDtos.CashDayCloseDtos;
using StoreManage.Shared.Models;
using static StoreManage.Shared.Utilitis.MyTypes;

namespace StoreManage.Server.Controllers.CashControlers.CashDayCloseControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashDayCloseController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public CashDayCloseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task< IActionResult> GetCashBallanceItemsForDate([FromBody] DayDto date)

        {
            string user = "User"; string bankAccount = "BanckAccount"; string customer = "Customer";string inCome = "InCome";
            string masterMoneySafe = "MasterMoneySafe"; string brancheMoneySafe = "BrancheMoneySafe";
            string employee = "Employee"; string outGoing = "OutGoing";  string Seller = "Seller"; 
            var include = new string[2];
            
            //(A) -  رصيد اليوم السابق
            // 1- الرصيد المبدئي لجميع خزن الفرع 
            var days = GetCashDays(date.Date , date.BrancheId);
            int maxId = days.Count == 0 ? 0 : days.Max(x => x.Id);
            // رصيد اخر عملية اقفال
            // في حالة عدم وجود يوم تغلاق سابق للفرع تكون قيمة اليوم السابق هي الرصيد المبدئي للخزن
            double lastDayBallance = maxId == 0 ? 0 : days.Where(x => x.Id == maxId).FirstOrDefault().RealAccountValue;



            //(B) - الايرادات 
            // 1- وارد من حساب بنكي
            include[0] = user; 
            include[1] = bankAccount;
            var cashInFromBankAccountA = await _unitOfWork.CashInFromBankAccount.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date , include);
            var cashInFromBankAccount = cashInFromBankAccountA.Select(r => new CashBallanceItem { Id = r.Id, Type = 0, Name = " صرف من حساب بنكي" + " " + r.BanckAccount.BankName + " " + r.BanckAccount.BankBrancheName + " حساب رقم" + r.BanckAccount.BankAccountNumber, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashInFromBankAccount.ToString(), UserCreate = r.User.FullName }).ToList();
            // 2- وارد من عميل

            include[1] = customer;

            var cashInFromCustomerA = await _unitOfWork.CashInFromCustomer.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date , include);
            var cashInFromCustomer = cashInFromCustomerA.Select(r => new CashBallanceItem { Id = r.Id, Type = 0, Name = "تحصيل من العميل" + " " + r.Customer.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashInFromCustomer.ToString(), UserCreate = r.User.FullName }).ToList();
            //3 - واردات من بنود الايرادات
            include[1] = inCome;

            var cashInFromIncomeA = await _unitOfWork.CashInFromIncome.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date , include);
            var cashInFromIncome = cashInFromIncomeA.Select(r => new CashBallanceItem { Id = r.Id, Type = 0, Name = " وارد من بند الايرادات " + r.InCome.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashInFromIncome.ToString() }).ToList();
            // 4- ايرادات من الخزينة الرئيسيه
            include[1] = masterMoneySafe;

            var cashInFromMasterMoneySafeA = await _unitOfWork.CashInFromMasterMoneySafe.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date,include);
            var cashInFromMasterMoneySafe = cashInFromMasterMoneySafeA.Select(r => new CashBallanceItem { Id = r.Id, Type = 0, Name = " وارد من الخزنه الرئيسيه " + r.MasterMoneySafe.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashInFromMasterMoneySafe.ToString(), UserCreate = r.User.FullName }).ToList();
            // 5- ايرادات من المبيعات
           


            var cashInFromordersA = await _unitOfWork.Order.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date);
            var cashInFromorders = new CashBallanceItem { Id = 0, Type = 0, Date = DateTime.Now, Name = "مبيعات", Value = cashInFromordersA.Sum(x => x.Paid), ItemType = CashItemTyps.Order.ToString() };

            // 6- ايرادات من مرتجع المشتريات

            var cashInFrompurchaseBacksA = await _unitOfWork.PurchaseBack.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date);
            var cashInFrompurchaseBacks = new CashBallanceItem { Id = 0, Type = 0, Date = DateTime.Now, Name = "مرتجع مشتريات", Value = cashInFrompurchaseBacksA.Sum(x => x.Paid), ItemType = CashItemTyps.PurshaseBack.ToString() };
            // 7- ايرادات من خزنة فرعيه
            include[1] = brancheMoneySafe;
            var cashInFromBrancheMoneySafeA = await _unitOfWork.CashInFromBrancheMoneySafe.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date ,include);
            var cashInFromBrancheMoneySafe = cashInFromBrancheMoneySafeA.Select(r => new CashBallanceItem { Id = r.Id, Type = 0, Name = $"(وارد من خزنة فرعيه)   {r.BrancheMoneySafe.Name}", Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashInFromBrancheMoneySafe.ToString(), UserCreate = r.User.FullName }).ToList();


            //(C) - المصروفات
            // 1 - صرف سلفة لموظف
            include[1] = employee;
            var cashOutToAdvancepaymentOfSalaryA = await _unitOfWork.CashOutToAdvancepaymentOfSalary.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date,include);
            var cashOutToAdvancepaymentOfSalary = cashOutToAdvancepaymentOfSalaryA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = "صرف سلفه " + " " + r.Employee.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToAdvancepaymentOfSalary.ToString(), UserCreate = r.User.FullName }).ToList();
            // 2 - صرف لحساب بنكي
            include[1] = bankAccount;

            var cashOutToBankAccountA = await _unitOfWork.CashOutToBankAccount.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date , include);
            var cashOutToBankAccount = cashOutToBankAccountA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = "ايداع  لحساب " + " " + r.BanckAccount.BankName + " فرع " + r.BanckAccount.BankBrancheName + " حساب رقم " + " " + r.BanckAccount.BankAccountNumber, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToBankAccount.ToString(), UserCreate = r.User.FullName }).ToList();
            // 3 - صرف للخزينة الرئيسية
            include[1] = masterMoneySafe;

            var cashOutToMasterMoneySafeA = await _unitOfWork.CashOutToMasterMoneySafe.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date, include);
            var cashOutToMasterMoneySafe = cashOutToMasterMoneySafeA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = "صرف للخزينة الرئيسيه" + " " + r.MasterMoneySafe.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToMasterMoneySafe.ToString(), UserCreate = r.User.FullName }).ToList();
            // 4 - صرف لبنود المصروفات
            include[1] = outGoing;

            var cashOutToOutGoingA = await _unitOfWork.CashOutToOutGoing.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date, include);
            var cashOutToOutGoing = cashOutToOutGoingA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = "صرف لبند المصروفات" + " " + r.OutGoing.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToOutGoing.ToString(), UserCreate = r.User.FullName }).ToList();
            // 5 - صرف المرتبات
            include[1] = employee;

            var cashOutToSalaryA = await _unitOfWork.CashOutToSalary.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.ProcessDate.Date == date.Date , include);
            var cashOutToSalary = cashOutToSalaryA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = "صرف مرتب" + " " + r.Employee.Name, Date = r.ProcessDate, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToSalary.ToString(), UserCreate = r.User.FullName }).ToList();
            // 6 - صرف لمورد
            include[1] = Seller;
            var cashOutToSellerA = await _unitOfWork.CashOutToSeller.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date , include);
            var cashOutToSeller = cashOutToSellerA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = "سداد للمورد" + " " + r.Seller.Name, Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToSeller.ToString(), UserCreate = r.User.FullName }).ToList();
            // 7- مصروفات الى مرتجع المبيعات

            var cashOutTOordersBackA = await _unitOfWork.OrderBack.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date);
            var cashOutTOordersBack = new CashBallanceItem { Id = 0, Type = 1, Date = DateTime.Now, Name = "مرتجع المبيعات", Value = cashOutTOordersBackA.Sum(x => x.RemainingAmount), ItemType = CashItemTyps.OrderBack.ToString() };

            // 8- مصروفات الى المشتريات

            var cashOutFrompurchasesA = await _unitOfWork.Purchase.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date);
            var cashOutFrompurchases = new CashBallanceItem { Id = 0, Type = 1, Date = DateTime.Now, Name = "المشتريات", Value = cashOutFrompurchasesA.Sum(x => x.RemainingAmount), ItemType = CashItemTyps.Purchase.ToString() };
            // 9- مصروفات الى خزنة فرعيه
            include[1] = brancheMoneySafe;

            var cashOutToBrancheMoneySafeA = await _unitOfWork.CashOutToBrancheMoneySafe.FindAllAsync(x => x.BrancheId == date.BrancheId && x.IsDeleted == false && x.Date.Date == date.Date, include);
            var cashOutToBrancheMoneySafe = cashOutToBrancheMoneySafeA.Select(r => new CashBallanceItem { Id = r.Id, Type = 1, Name = $"(ايداع الى خزنة فرعيه)   {r.BrancheMoneySafe.Name}", Date = r.Date, Description = r.Notes, Value = r.Value, ItemType = CashItemTyps.cashOutToBrancheMoneySafe.ToString(), UserCreate = r.User.FullName }).ToList();


            var cashdayClos = _unitOfWork.CashDayClose.Find(x => x.BrancheId == date.BrancheId && x.DayCloseDate.Date == date.Date);

            List<CashBallanceItem> allItems = new List<CashBallanceItem>();
            if (cashInFromorders.Value != 0) allItems.Add(cashInFromorders);
            if (cashOutTOordersBack.Value != 0) allItems.Add(cashOutTOordersBack);
            if (cashInFrompurchaseBacks.Value != 0) allItems.Add(cashInFrompurchaseBacks);
            if (cashOutFrompurchases.Value != 0) allItems.Add(cashOutFrompurchases);
            allItems.AddRange(cashInFromMasterMoneySafe);
            allItems.AddRange(cashInFromBrancheMoneySafe);
            allItems.AddRange(cashInFromCustomer);
            allItems.AddRange(cashInFromBankAccount);
            allItems.AddRange(cashInFromIncome);
            allItems.AddRange(cashOutToMasterMoneySafe);
            allItems.AddRange(cashOutToBrancheMoneySafe);
            allItems.AddRange(cashOutToSeller);
            allItems.AddRange(cashOutToOutGoing);
            allItems.AddRange(cashOutToAdvancepaymentOfSalary);
            allItems.AddRange(cashOutToSalary);
            allItems.AddRange(cashOutToBankAccount);



            var xxx =  new CashBallanceDay
            {
                Items = allItems,
                Values = new CashBallanceValues
                {
                    lastDayBallance = lastDayBallance,
                    cashInFromBankAccount = cashInFromBankAccount.Sum(r => r.Value),
                    cashInFromCustomer = cashInFromCustomer.Sum(r => r.Value),
                    cashInFromIncome = cashInFromIncome.Sum(r => r.Value),
                    cashInFromMasterMoneySafe = cashInFromMasterMoneySafe.Sum(r => r.Value),
                    cashInFromBrancheMoneySafe = cashInFromBrancheMoneySafe.Sum(r => r.Value),
                    cashOutToAdvancepaymentOfSalary = cashOutToAdvancepaymentOfSalary.Sum(r => r.Value),
                    cashOutToBankAccount = cashOutToBankAccount.Sum(r => r.Value),
                    cashOutToMasterMoneySafe = cashOutToMasterMoneySafe.Sum(r => r.Value),
                    cashOutToBrancheMoneySafe = cashOutToBrancheMoneySafe.Sum(r => r.Value),
                    cashOutToOutGoing = cashOutToOutGoing.Sum(r => r.Value),
                    cashOutToSalary = cashOutToSalary.Sum(r => r.Value),
                    cashOutToSeller = cashOutToSeller.Sum(r => r.Value),
                    CashInFromOrders = cashInFromorders.Value,
                    CashOutToOrderBacks = cashOutTOordersBack.Value,
                    CashInFromPurcaseBacks = cashInFrompurchaseBacks.Value,
                    CashOutToOrderPurchase = cashOutFrompurchases.Value
                },
                RealCash = cashdayClos == null ? 0 : cashdayClos.RealAccountValue,
            };

            return Ok(xxx);

        }



        private List<CashDayClose> GetCashDays(DateTime date, int brancheId)
        {
            return _unitOfWork.CashDayClose.FindAll(x => x.BrancheId == brancheId && x.DayCloseDate.Date < date.Date).ToList();
        }
       
    }
}
