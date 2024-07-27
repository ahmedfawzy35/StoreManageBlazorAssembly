using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CashDtos.CashInDtos;
using StoreManage.Shared.Dtos.CashDtos.CashOutDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CashControlers.CashOutControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashOutToBrancheMoneySafeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashOutToBrancheMoneySafeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[3];
            include[0] = "BrancheMoneySafe";
            include[1] = "Branche";
            include[2] = "User";
            var ci = _unitOfWork.CashOutToBrancheMoneySafe.FindAll(x => x.BrancheId == brancheId && !x.IsDeleted, include);
            return Ok(ToCashOutToBrancheMoneySafeDto(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[3];
            include[0] = "BrancheMoneySafe";
            include[1] = "Branche";
            include[2] = "User";
            var c = _unitOfWork.CashOutToBrancheMoneySafe.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
            }
            var cdto = new CashOutToBrancheMoneySafeDto
            {
                BrancheId = c.BrancheId,
                BrancheName = c.Branche.Name,
                BrancheMoneySafeId = c.BrancheMoneySafeId,
                BrancheMoneySafeName = c.BrancheMoneySafe.Name,
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
        public async Task<IActionResult> Add([FromBody] CashOutToBrancheMoneySafeDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new CashOutToBrancheMoneySafe();
                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.BrancheMoneySafeId = model.BrancheMoneySafeId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;

                try
                {
                    myCash = await _unitOfWork.CashOutToBrancheMoneySafe.AddAsync(myCash);
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
        public IActionResult Edit([FromBody] CashOutToBrancheMoneySafeDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.CashOutToBrancheMoneySafe.GetById(model.Id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }

                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.BrancheMoneySafeId = model.BrancheMoneySafeId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;
                try
                {
                    _unitOfWork.CashOutToBrancheMoneySafe.Update(myCash);
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
                var myCash = _unitOfWork.CashOutToBrancheMoneySafe.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }
                _unitOfWork.CashOutToBrancheMoneySafe.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف العملية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف العملية ");
            }
        }

        private List<CashOutToBrancheMoneySafeDto> ToCashOutToBrancheMoneySafeDto(List<CashOutToBrancheMoneySafe> source)
        {
            List<CashOutToBrancheMoneySafeDto> list = new List<CashOutToBrancheMoneySafeDto>();

            foreach (var c in source)
            {
                list.Add(new CashOutToBrancheMoneySafeDto
                {
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name,
                    BrancheMoneySafeId = c.BrancheMoneySafeId,
                    BrancheMoneySafeName = c.BrancheMoneySafe.Name,
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
