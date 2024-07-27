using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CashDtos.CashOutDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CashControlers.CashOutControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CashOutToSalaryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashOutToSalaryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[3];
            include[0] = "Employee";
            include[1] = "Branche";
            include[2] = "User";
            var ci = _unitOfWork.CashOutToSalary.FindAll(x => x.BrancheId == brancheId && !x.IsDeleted, include);
            return Ok(CashOutToSalaryDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[3];
            include[0] = "Employee";
            include[1] = "Branche";
            include[2] = "User";
            var c = _unitOfWork.CashOutToSalary.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد العملية في قاعدة البيانات");
            }
            var cdto = new CashOutToSalaryDto
            {
                BrancheId = c.BrancheId,
                BrancheName = c.Branche.Name,
                EmployeeId = c.EmployeeId,
                EmployeeName = c.Employee.Name,
                DueDate = c.DueDate,
                ProcessDate = c.ProcessDate,
                Id = c.Id,
                Notes = c.Notes,
                UserFullName = c.User.FullName,
                UserId = c.User.Id,
                Value = c.Value,

            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CashOutToSalaryDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new CashOutToSalary();
                myCash.DueDate = model.DueDate;
                myCash.ProcessDate = model.ProcessDate;
                myCash.Date = model.ProcessDate;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.EmployeeId = model.EmployeeId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;
                myCash.BrancheId = model.BrancheId;

                try
                {
                    myCash = await _unitOfWork.CashOutToSalary.AddAsync(myCash);
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
        public IActionResult Edit([FromBody] CashOutToSalaryDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.CashOutToSalary.GetById(model.Id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }

                myCash.ProcessDate = model.ProcessDate;
                myCash.DueDate = model.DueDate;
                myCash.Date = model.ProcessDate;
                myCash.Value = model.Value;
                myCash.Notes = model.Notes;
                myCash.EmployeeId = model.EmployeeId;
                myCash.UserId = model.UserId;
                myCash.BrancheId = model.BrancheId;
                myCash.BrancheId = model.BrancheId;
                try
                {
                    _unitOfWork.CashOutToSalary.Update(myCash);
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
                var myCash = _unitOfWork.CashOutToSalary.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على العملية ");
                }
                _unitOfWork.CashOutToSalary.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف العملية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف العملية ");
            }
        }

        private List<CashOutToSalaryDto> CashOutToSalaryDtos(List<CashOutToSalary> source)
        {
            List<CashOutToSalaryDto> list = new List<CashOutToSalaryDto>();

            foreach (var c in source)
            {
                list.Add(new CashOutToSalaryDto
                {
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name,
                    EmployeeId = c.EmployeeId,
                    EmployeeName = c.Employee.Name,
                    DueDate = c.DueDate,
                    ProcessDate = c.ProcessDate,    
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
