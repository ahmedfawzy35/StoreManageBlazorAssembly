using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CashDtos.CashInDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CashControlers.CashInControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashInFromBankAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashInFromBankAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[3];
            include[0] = "BankAccount";
            include[1] = "Branche";
            include[2] = "User";
            var ci = _unitOfWork.CashInFromBankAccount.FindAll(x => x.BrancheId == brancheId && !x.IsDeleted, include);
            return Ok(ToCashInFromBankAccountDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[3];
            include[0] = "BankAccount";
            include[1] = "Branche";
            include[2] = "User";
            var c = _unitOfWork.CashInFromBankAccount.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
            }
            var cdto = new CashInFromBankAccountDto
            {
                BrancheId = c.BrancheId,
                BrancheName = c.Branche.Name,
                BanckAccountId = c.BanckAccountId,
                BanckAccountName = c.BanckAccount.BankName,
                BanckAccountBrancheName = c.BanckAccount.BankBrancheName,
                Date = c.Date,
                Id = c.Id,
                Notes = c.Notes,
                UserFullName = c.User.FullName,
                UserId = c.User.Id,
                Value = c.Value,

            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CashInFromBankAccountDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new CashInFromBankAccount();
                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.BanckAccountId = model.BanckAccountId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;
                myCash.BrancheId = model.BrancheId;

                try
                {
                    myCash = await _unitOfWork.CashInFromBankAccount.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة العملية ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] CashInFromBankAccountDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.CashInFromBankAccount.GetById(model.Id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }

                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.BanckAccountId = model.BanckAccountId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;
                myCash.BrancheId = model.BrancheId;
                try
                {
                    _unitOfWork.CashInFromBankAccount.Update(myCash);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل العملية");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var myCash = _unitOfWork.CashInFromBankAccount.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }
                _unitOfWork.CashInFromBankAccount.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف العملية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف العملية ");
            }
        }

        private List<CashInFromBankAccountDto> ToCashInFromBankAccountDtos(List<CashInFromBankAccount> source)
        {
            List<CashInFromBankAccountDto> list = new List<CashInFromBankAccountDto>();

            foreach (var c in source)
            {
                list.Add(new CashInFromBankAccountDto
                {
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name,
                    BanckAccountId = c.BanckAccountId,
                    BanckAccountName = c.BanckAccount.BankName,
                    BanckAccountBrancheName = c.BanckAccount.BankBrancheName,
                    Date = c.Date,
                    Id = c.Id,
                    Notes = c.Notes,
                    UserFullName = c.User.FullName,
                    UserId = c.User.Id,
                    Value = c.Value,

                });
            }
            return list;
        }

    }
}
