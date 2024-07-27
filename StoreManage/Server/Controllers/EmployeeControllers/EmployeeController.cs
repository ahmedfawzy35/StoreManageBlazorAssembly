using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CashDtos.CashInDtos;
using StoreManage.Shared.Dtos.EmployeeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.EmployeeControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[1];
            include[0] = "Branche";

            var ci = _unitOfWork.Employee.FindAll(x => x.BrancheId == brancheId , include);
            return Ok(ToEmployeeDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[1];
            include[0] = "Branche";

            var c = _unitOfWork.Employee.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد الموظف في قاعدة البيانات");
            }
            var cdto = new EmployeeDto
            {
                Id = c.Id,
                BrancheId = c.BrancheId,
                BrancheName = c.Branche.Name,
                Name = c.Name,
                Adress = c.Adress,
                DateEnd = c.DateEnd,
                DateStart = c.DateStart,
                Enabled = c.Enabled,
                Phone = c.Phone,
                Salary = c.Salary,


            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new Employee();
                myCash.BrancheId = model.BrancheId;
                myCash.Name = model.Name;
                myCash.Adress = model.Adress;
                myCash.DateStart = model.DateStart;
                myCash.DateEnd = model.DateEnd;
                myCash.Enabled = model.Enabled;
                myCash.Phone = model.Phone;
                myCash.Salary = model.Salary;

                try
                {
                    myCash = await _unitOfWork.Employee.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة الموظف ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] EmployeeDto model)
        {


            if (ModelState.IsValid)
            {
                var myCash = _unitOfWork.Employee.GetById(model.Id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على الموظف ");
                }

                myCash.BrancheId = model.BrancheId;
                myCash.Name = model.Name;
                myCash.Adress = model.Adress;
                myCash.DateStart = model.DateStart;
                myCash.DateEnd = model.DateEnd;
                myCash.Enabled = model.Enabled;
                myCash.Phone = model.Phone;
                myCash.Salary = model.Salary;
                try
                {
                    _unitOfWork.Employee.Update(myCash);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل الموظف");
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
                var myCash = _unitOfWork.Employee.GetById(id);
                if (myCash == null)
                {
                    return BadRequest("لم يتم العثور على الموظف ");
                }
                _unitOfWork.Employee.Delete(myCash);
                _unitOfWork.Complete();
                return Ok("تم حذف الموظف ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف الموظف ");
            }
        }

        private List<EmployeeDto> ToEmployeeDtos(List<Employee> source)
        {
            List<EmployeeDto> list = new List<EmployeeDto>();

            foreach (var c in source)
            {
                list.Add(new EmployeeDto
                {
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name,
                    Name = c.Name,
                    Adress = c.Adress,
                    DateEnd = c.DateEnd,
                    DateStart = c.DateStart,
                    Id = c.Id,
                    Enabled = c.Enabled,
                    Phone = c.Phone,
                    Salary = c.Salary,
                    

                });
            }
            return list;
        }
    }
}
