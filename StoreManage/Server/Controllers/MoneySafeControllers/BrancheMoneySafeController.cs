using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.BrancheMoneySafeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.MoneySafeControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrancheMoneySafeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrancheMoneySafeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[1];
            include[0] = "Branche";
            var ci = _unitOfWork.BrancheMoneySafe.FindAll(x => x.BrancheId == brancheId, include);
            return Ok(ToBrancheMoneySafeDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[1];
            include[0] = "Branche";
            var c = _unitOfWork.BrancheMoneySafe.Find(x => x.Id == id,include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد الخزينة الفرعية في قاعدة البيانات");
            }
            var cdto = new BrancheMoneySafeDto
            {
                Id = c.Id,
                Name = c.Name!,
                Notes = c.Notes,
                StartAccount = c.StartAccount,
                BrancheId = c.BrancheId,
                BrancheName =c.Branche.Name



            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BrancheMoneySafeDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new BrancheMoneySafe
                {
                    Name = model.Name,
                    Notes = model.Notes,
                    StartAccount = model.StartAccount,
                    BrancheId = model.BrancheId,
                   
                };

                try
                {
                    myCash = await _unitOfWork.BrancheMoneySafe.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة الخزينة الفرعية ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] BrancheMoneySafeDto model)
        {


            if (ModelState.IsValid)
            {
                var BrancheMoneySafe = _unitOfWork.BrancheMoneySafe.GetById(model.Id);
                if (BrancheMoneySafe == null)
                {
                    return BadRequest("لم يتم العثور على الخزينة الفرعية ");
                }

                BrancheMoneySafe.Name = model.Name;
                BrancheMoneySafe.Notes = model.Notes;
                BrancheMoneySafe.StartAccount = model.StartAccount;
                BrancheMoneySafe.BrancheId = model.BrancheId;


                try
                {
                    _unitOfWork.BrancheMoneySafe.Update(BrancheMoneySafe);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل الخزينة الفرعية");
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
                var BrancheMoneySafe = _unitOfWork.BrancheMoneySafe.GetById(id);
                if (BrancheMoneySafe == null)
                {
                    return BadRequest("لم يتم العثور على الخزينة الفرعية ");
                }
                _unitOfWork.BrancheMoneySafe.Delete(BrancheMoneySafe);
                _unitOfWork.Complete();
                return Ok("تم حذف الخزينة الفرعية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف الخزينة الفرعية ");
            }
        }

        private List<BrancheMoneySafeDto> ToBrancheMoneySafeDtos(List<BrancheMoneySafe> source)
        {
            List<BrancheMoneySafeDto> list = new List<BrancheMoneySafeDto>();

            foreach (var c in source)
            {
                list.Add(new BrancheMoneySafeDto
                {

                    Id = c.Id,
                    Name = c.Name,
                    Notes = c.Notes,
                    StartAccount = c.StartAccount,
                     BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name


                });
            }
            return list;
        }
    }
}
