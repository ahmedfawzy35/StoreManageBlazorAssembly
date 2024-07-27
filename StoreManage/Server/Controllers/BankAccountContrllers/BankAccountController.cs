using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.BankAccountDtos;
using StoreManage.Shared.Dtos.CashDtos.CashInDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.BankAccountContrllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()

        {
            
            var ci = _unitOfWork.BankAccount.GetAll();
            return Ok(ToBankAccountDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {

            var c = _unitOfWork.BankAccount.Find(x => x.Id == id);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد الحساب البنكي في قاعدة البيانات");
            }
            var cdto = new BankAccountDto
            {
                Id = c.Id,
                BankName = c.BankName,
                BankAccountNumber = c.BankAccountNumber,
                BankBrancheName = c.BankBrancheName,
                StartAccount = c.StartAccount,
                Notes = c.Notes,


            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BankAccountDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new   BankAccount
                {
                    BankName = model.BankName,
                    BankAccountNumber = model.BankAccountNumber,
                    BankBrancheName = model.BankBrancheName,
                    StartAccount = model.StartAccount,
                    Notes = model.Notes,
                    

                };

                try
                {
                    myCash = await _unitOfWork.BankAccount.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة الحساب البنكي ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] BankAccountDto model)
        {


            if (ModelState.IsValid)
            {
                var bankAccount = _unitOfWork.BankAccount.GetById(model.Id);
                if (bankAccount == null)
                {
                    return BadRequest("لم يتم العثور على الحساب البنكي ");
                }

                bankAccount.BankName = model.BankName;
                bankAccount.BankAccountNumber = model.BankAccountNumber;
                bankAccount.BankBrancheName = model.BankBrancheName;
                bankAccount.Notes = model.Notes;
                bankAccount.StartAccount = model.StartAccount;
              
                try
                {
                    _unitOfWork.BankAccount.Update(bankAccount);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل الحساب البنكي");
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
                var bankAccount = _unitOfWork.BankAccount.GetById(id);
                if (bankAccount == null)
                {
                    return BadRequest("لم يتم العثور على الحساب البنكي ");
                }
                _unitOfWork.BankAccount.Delete(bankAccount);
                _unitOfWork.Complete();
                return Ok("تم حذف الحساب البنكي ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف الحساب البنكي ");
            }
        }

        private List<BankAccountDto> ToBankAccountDtos(List<BankAccount> source)
        {
            List<BankAccountDto> list = new List<BankAccountDto>();

            foreach (var c in source)
            {
                list.Add(new BankAccountDto
                {

                    Id = c.Id,
                    BankName = c.BankName,
                    BankAccountNumber = c.BankAccountNumber,
                    BankBrancheName = c.BankBrancheName,
                    StartAccount = c.StartAccount,
                    Notes = c.Notes,


                });
            }
            return list;
        }
    }
}
