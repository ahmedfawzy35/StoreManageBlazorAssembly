using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CashDtos.CashOutDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CashControlers.CashOutControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashOutToBankAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashOutToBankAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[3];
            include[0] = "BanckAccount";
            include[1] = "Branche";
            include[2] = "User";
            var ci = _unitOfWork.CashOutToBankAccount.FindAll(x => x.BrancheId == brancheId && (x.IsDeleted == null? true :x.IsDeleted == false), include);
            if (ci != null)
                return Ok(ToCashOutToBankAccountDtos(ci.ToList()));
            else return BadRequest("لا يوجد بيانات");

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[3];
            include[0] = "BanckAccount";
            include[1] = "Branche";
            include[2] = "User";
            try
            {
                var c = _unitOfWork.CashOutToBankAccount.Find(x => x.Id == id, include);

                if (c is null)
                {
                    return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
                }
                var cdto = new CashOutToBankAccountDto
                {
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name,
                    BanckAccountId = c.BanckAccountId,
                    BanckAccountName = c.BanckAccount.BankName,
                    BanckBrancheName = c.BanckAccount.BankBrancheName,
                    Date = c.Date,
                    Id = c.Id,
                    Notes = c.Notes,
                    UserFullName = c.User.FullName,
                    UserId = c.User.Id,
                    Value = c.Value,

                };
                return Ok(cdto);
            }
            catch (Exception)
            {

                return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CashOutToBankAccountDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new CashOutToBankAccount();
                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.BanckAccountId = model.BanckAccountId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;
                myCash.BrancheId = model.BrancheId;

                try
                {
                    myCash = await _unitOfWork.CashOutToBankAccount.AddAsync(myCash);
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
        public IActionResult Edit([FromBody] CashOutToBankAccountDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.CashOutToBankAccount.GetById(model.Id);
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
                    _unitOfWork.CashOutToBankAccount.Update(myCash);
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
                var myCash = _unitOfWork.CashOutToBankAccount.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }
                _unitOfWork.CashOutToBankAccount.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف العملية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف العملية ");
            }
        }

        private List<CashOutToBankAccountDto> ToCashOutToBankAccountDtos(List<CashOutToBankAccount> source)
        {
            List<CashOutToBankAccountDto> list = new List<CashOutToBankAccountDto>();

            foreach (var c in source)
            {
                list.Add(new CashOutToBankAccountDto
                {
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name,
                    BanckAccountId = c.BanckAccountId,
                    BanckAccountName = c.BanckAccount.BankName,
                    BanckBrancheName = c.BanckAccount.BankBrancheName,
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
