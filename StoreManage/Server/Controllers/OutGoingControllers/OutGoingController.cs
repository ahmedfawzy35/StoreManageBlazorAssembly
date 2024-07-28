using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.OutGoingDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.OutGoingControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OutGoingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OutGoingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[1];
            include[0] = "Branche";

            var ci = _unitOfWork.OutGoing.FindAll(x => x.BrancheId == brancheId, include);
            return Ok(ToOutGoingDtos(ci.ToList()));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            var include = new string[1];
            include[0] = "Branche";
            var c = _unitOfWork.OutGoing.Find(x => x.Id == id ,include);
            if (c is null)
            {
                return BadRequest("لم يتم ايجاد بند المصروفات في قاعدة البيانات");
            }
            var cdto = new OutGoingDto
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
        public async Task<IActionResult> Add([FromBody] OutGoingDto model)
        {
            if (ModelState.IsValid)
            {
                var brance = _unitOfWork.Branche.GetById(model.BrancheId);
                var myCash = new OutGoing
                {
                    Name = model.Name,
                    Notes = model.Notes,
                    BrancheId = model.BrancheId,

                };

                try
                {
                    myCash = await _unitOfWork.OutGoing.AddAsync(myCash);
                    _unitOfWork.Complete();
                    model.BrancheName = brance.Name;

                    model.Id = myCash.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضافة بند المصروفات ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        [HttpPut()]
        public IActionResult Edit([FromBody] OutGoingDto model)
        {


            if (ModelState.IsValid)
            {
                var brance = _unitOfWork.Branche.GetById(model.BrancheId);

                var OutGoing = _unitOfWork.OutGoing.GetById(model.Id);
                if (OutGoing == null)
                {
                    return BadRequest("لم يتم العثور على بند المصروفات ");
                }

                OutGoing.Name = model.Name;
                OutGoing.Notes = model.Notes;
                OutGoing.BrancheId = model.BrancheId;


                try
                {
                    _unitOfWork.OutGoing.Update(OutGoing);
                    _unitOfWork.Complete();
                    model.BrancheName = brance.Name;

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل بند المصروفات");
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
                var OutGoing = _unitOfWork.OutGoing.GetById(id);
                if (OutGoing == null)
                {
                    return BadRequest("لم يتم العثور على بند المصروفات ");
                }
                _unitOfWork.OutGoing.Delete(OutGoing);
                _unitOfWork.Complete();
                return Ok("تم حذف بند المصروفات ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف بند المصروفات ");
            }
        }

        private List<OutGoingDto> ToOutGoingDtos(List<OutGoing> source)
        {
            List<OutGoingDto> list = new List<OutGoingDto>();

            foreach (var c in source)
            {
                list.Add(new OutGoingDto
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
