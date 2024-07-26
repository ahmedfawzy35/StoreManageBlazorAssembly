using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CashDtos.CashInDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CashControlers.CashInControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashInFromBrancheMoneySafeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashInFromBrancheMoneySafeController(IUnitOfWork unitOfWork)
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
            var ci = _unitOfWork.CashInFromBrancheMoneySafe.FindAll(x => x.BrancheId == brancheId && !x.IsDeleted, include);
            return Ok(ToCashInFromBrancheMoneySafeDto(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[3];
            include[0] = "BrancheMoneySafe";
            include[1] = "Branche";
            include[2] = "User";
            var c = _unitOfWork.CashInFromBrancheMoneySafe.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
            }
            var cdto = new CashInFromBrancheMoneySafeDto
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
        public async Task<IActionResult> Add([FromBody] CashInFromBrancheMoneySafeDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new CashInFromBrancheMoneySafe();
                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.BrancheMoneySafeId = model.BrancheMoneySafeId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;

                try
                {
                    myCash = await _unitOfWork.CashInFromBrancheMoneySafe.AddAsync(myCash);
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
        public IActionResult Edit([FromBody] CashInFromBrancheMoneySafeDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.CashInFromBrancheMoneySafe.GetById(model.Id);
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
                    _unitOfWork.CashInFromBrancheMoneySafe.Update(myCash);
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
                var myCash = _unitOfWork.CashInFromBrancheMoneySafe.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }
                _unitOfWork.CashInFromBrancheMoneySafe.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف العملية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف العملية ");
            }
        }

        private List<CashInFromBrancheMoneySafeDto> ToCashInFromBrancheMoneySafeDto(List<CashInFromBrancheMoneySafe> source)
        {
            List<CashInFromBrancheMoneySafeDto> list = new List<CashInFromBrancheMoneySafeDto>();

            foreach (var c in source)
            {
                list.Add(new CashInFromBrancheMoneySafeDto
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
