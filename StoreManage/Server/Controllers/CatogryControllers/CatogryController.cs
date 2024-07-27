using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CatogryDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CatogryControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatogryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatogryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()

        {
           
            var ci = _unitOfWork.Catogry.GetAll();
            return Ok(ToCatogryDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
          
            var c = _unitOfWork.Catogry.Find(x => x.Id == id);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد الصنف في قاعدة البيانات");
            }
            var cdto = new CatogryDto
            {
                Id = c.Id,
               Name = c.Name,
               Details = c.Details,
              


            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CatogryDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new Catogry
                {
                   Name = model.Name,
                   Details = model.Details,

                };

                try
                {
                    myCash = await _unitOfWork.Catogry.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة الصنف ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] CatogryDto model)
        {


            if (ModelState.IsValid)
            {
                var catogry = _unitOfWork.Catogry.GetById(model.Id);
                if (catogry == null)
                {
                    return BadRequest("لم يتم العثور على الصنف ");
                }

                catogry.Name = model.Name;
                catogry.Details = model.Details;
                

                try
                {
                    _unitOfWork.Catogry.Update(catogry);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل الصنف");
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
                var Catogry = _unitOfWork.Catogry.GetById(id);
                if (Catogry == null)
                {
                    return BadRequest("لم يتم العثور على الصنف ");
                }
                _unitOfWork.Catogry.Delete(Catogry);
                _unitOfWork.Complete();
                return Ok("تم حذف الصنف ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف الصنف ");
            }
        }

        private List<CatogryDto> ToCatogryDtos(List<Catogry> source)
        {
            List<CatogryDto> list = new List<CatogryDto>();

            foreach (var c in source)
            {
                list.Add(new CatogryDto
                {

                    Id = c.Id,
                    Name = c.Name,
                    Details = c.Details,
                   


                });
            }
            return list;
        }
    }
}
