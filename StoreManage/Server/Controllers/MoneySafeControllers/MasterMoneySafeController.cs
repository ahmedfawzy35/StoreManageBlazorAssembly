using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.MasterMoneySafeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.MoneySafeControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterMoneySafeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterMoneySafeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()

        {

            var ci = _unitOfWork.MasterMoneySafe.GetAll();
            return Ok(ToMasterMoneySafeDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {

            var c = _unitOfWork.MasterMoneySafe.Find(x => x.Id == id);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد الخزينة الرئيسية في قاعدة البيانات");
            }
            var cdto = new MasterMoneySafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Notes = c.Notes,
                StartAccount = c.StartAccount,



            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MasterMoneySafeDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new MasterMoneySafe
                {
                    Name = model.Name,
                    Notes = model.Notes,
                    StartAccount = model.StartAccount,

                };

                try
                {
                    myCash = await _unitOfWork.MasterMoneySafe.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة الخزينة الرئيسية ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] MasterMoneySafeDto model)
        {


            if (ModelState.IsValid)
            {
                var MasterMoneySafe = _unitOfWork.MasterMoneySafe.GetById(model.Id);
                if (MasterMoneySafe == null)
                {
                    return BadRequest("لم يتم العثور على الخزينة الرئيسية ");
                }

                MasterMoneySafe.Name = model.Name;
                MasterMoneySafe.Notes = model.Notes;
                MasterMoneySafe.StartAccount = model.StartAccount;


                try
                {
                    _unitOfWork.MasterMoneySafe.Update(MasterMoneySafe);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل الخزينة الرئيسية");
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
                var MasterMoneySafe = _unitOfWork.MasterMoneySafe.GetById(id);
                if (MasterMoneySafe == null)
                {
                    return BadRequest("لم يتم العثور على الخزينة الرئيسية ");
                }
                _unitOfWork.MasterMoneySafe.Delete(MasterMoneySafe);
                _unitOfWork.Complete();
                return Ok("تم حذف الخزينة الرئيسية ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف الخزينة الرئيسية ");
            }
        }

        private List<MasterMoneySafeDto> ToMasterMoneySafeDtos(List<MasterMoneySafe> source)
        {
            List<MasterMoneySafeDto> list = new List<MasterMoneySafeDto>();

            foreach (var c in source)
            {
                list.Add(new MasterMoneySafeDto
                {

                    Id = c.Id,
                    Name = c.Name,
                    Notes = c.Notes,
                    StartAccount = c.StartAccount



                });
            }
            return list;
        }
    }
}
