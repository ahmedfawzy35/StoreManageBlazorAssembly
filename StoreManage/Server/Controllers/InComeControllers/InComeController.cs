using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.IncomeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.InComeControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InComeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InComeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[1];
            include[0] = "Branche";

            var ci = _unitOfWork.InCome.FindAll(x => x.BrancheId == brancheId, include);
            return Ok(ToInComeDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[1];
            include[0] = "Branche";
            var c = _unitOfWork.InCome.Find(x => x.Id == id, include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد بند الايرادات في قاعدة البيانات");
            }
            var cdto = new InComeDto
            {
                Id = c.Id,
                Name = c.Name,
                Notes = c.Notes,
                BrancheId = c.BrancheId,
                BrancheName = c.Branche.Name,



            };
            return Ok(cdto);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InComeDto model)
        {
            if (ModelState.IsValid)
            {
                var myCash = new InCome
                {
                    Name = model.Name,
                    Notes = model.Notes,
                    BrancheId = model.BrancheId,

                };

                try
                {
                    myCash = await _unitOfWork.InCome.AddAsync(myCash);
                    _unitOfWork.Complete();

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة بند الايرادات ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] InComeDto model)
        {


            if (ModelState.IsValid)
            {
                var InCome = _unitOfWork.InCome.GetById(model.Id);
                if (InCome == null)
                {
                    return BadRequest("لم يتم العثور على بند الايرادات ");
                }

                InCome.Name = model.Name;
                InCome.Notes = model.Notes;
                InCome.BrancheId = model.BrancheId;


                try
                {
                    _unitOfWork.InCome.Update(InCome);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل بند الايرادات");
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
                var InCome = _unitOfWork.InCome.GetById(id);
                if (InCome == null)
                {
                    return BadRequest("لم يتم العثور على بند الايرادات ");
                }
                _unitOfWork.InCome.Delete(InCome);
                _unitOfWork.Complete();
                return Ok("تم حذف بند الايرادات ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف بند الايرادات ");
            }
        }

        private List<InComeDto> ToInComeDtos(List<InCome> source)
        {
            List<InComeDto> list = new List<InComeDto>();

            foreach (var c in source)
            {
                list.Add(new InComeDto
                {

                    Id = c.Id,
                    Name = c.Name,
                    Notes = c.Notes,
                    BrancheId = c.BrancheId,
                    BrancheName = c.Branche.Name



                });
            }
            return list;
        }
    }
}
