using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.EmployeeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.EmployeeIncreaseControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeIncreaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeIncreaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[2];
            include[0] = "Employee";
            include[1] = "User";

            var ci = _unitOfWork.EmployeeIncrease.FindAll(x => x.Employee.BrancheId == brancheId, include);
            return Ok(ToEmployeeProcessDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[2];
            include[0] = "Employee";
            include[1] = "User";

            var c = _unitOfWork.EmployeeIncrease.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
            }
            var branches = _unitOfWork.Branche.GetAll();

            var cdto = new EmployeeProcessDto
            {
                Id = c.Id,
                Date = c.Date,
                Value = c.Value,
                Notes = c.Notes,
                BrancheName = branches.Where(x => x.Id == c.Employee.BrancheId).FirstOrDefault() is null ? " " : branches.Where(x => x.Id == c.Employee.BrancheId).FirstOrDefault().Name,
                UserFullName = c.User.FullName,
                EmployeeId = c.EmployeeId,
                EmployeeName = c.Employee.Name,
                UserId = c.UserId


            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EmployeeProcessDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new EmployeeIncrease();
                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.UserId = model.UserId;
                myCash.EmployeeId = model.EmployeeId;
              

                try
                {
                    myCash = await _unitOfWork.EmployeeIncrease.AddAsync(myCash);
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
        public IActionResult Edit([FromBody] EmployeeProcessDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.EmployeeIncrease.GetById(model.Id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }

                myCash.Date = model.Date;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.UserId = model.UserId;
                myCash.EmployeeId = model.EmployeeId;
                try
                {
                    _unitOfWork.EmployeeIncrease.Update(myCash);
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
                var myCash = _unitOfWork.EmployeeIncrease.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }
                _unitOfWork.EmployeeIncrease.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف العملية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف العملية ");
            }
        }

        private List<EmployeeProcessDto> ToEmployeeProcessDtos(List<EmployeeIncrease> source)
        {
            List<EmployeeProcessDto> list = new List<EmployeeProcessDto>();
            var branches = _unitOfWork.Branche.GetAll();
            foreach (var c in source)
            {
                list.Add(new EmployeeProcessDto
                {
                    Id = c.Id,
                    Date = c.Date,
                    Value = c.Value,
                    Notes = c.Notes,
                    BrancheName = branches.Where(x => x.Id == c.Employee.BrancheId).FirstOrDefault() is null ?" " : branches.Where(x=>x.Id == c.Employee.BrancheId).FirstOrDefault().Name,
                    UserFullName = c.User.FullName,
                    EmployeeId = c.EmployeeId,
                    EmployeeName = c.Employee.Name,
                    UserId = c.UserId


                });
            }
            return list;
        }
    }
}
