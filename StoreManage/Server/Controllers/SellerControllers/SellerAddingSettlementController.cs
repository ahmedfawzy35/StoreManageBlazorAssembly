using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.SellerDato.SellerSettlementDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.SellerControllers
{
    [Route("api/[controller]/[action]")]

    [ApiController]
    public class SellerAddingSettlementController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;

        public SellerAddingSettlementController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        // GET: api/<SellerController>

        [HttpGet]
        public IActionResult GetAll()

        {
            var include = new string[1];
            include[0] = "Seller";
            var cas = _UnitOfWork.SellerAddingSettlement.FindAll(x => true, include);
            return Ok(ToDto(cas.ToList()));
        }
        [HttpGet]
        public IActionResult GetAllForBranche([FromBody]int brancheId)

        {
            var include =new string[1];
            include[0] = "Seller";
            var cas = _UnitOfWork.SellerAddingSettlement.FindAll( x=> x.BrancheId == brancheId , include);
            return Ok(ToDto(cas.ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cas = _UnitOfWork.SellerAddingSettlement.GetById(id);
            if (cas != null)
            {
                var seller = _UnitOfWork.Seller.GetById(cas.SellerId);
                var casDto = new SellerAddingSettlementDto();
                casDto.Id = cas.Id;
                casDto.UserId = cas.UserId;
                casDto.BrancheId = cas.BrancheId;
                casDto.SellerId = cas.SellerId;
                casDto.Date = cas.Date;
                casDto.Value = cas.Value;
                casDto.Notes = cas.Notes;
                casDto.SellerName = seller == null ? " " : seller.Name;

                return Ok(casDto);
            }
            else
            {
                return BadRequest("لم يتم ايجاد البيانات");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] SellerAddingSettlementDto model)
        {
            if (ModelState.IsValid)
            {
                var cas = new SellerAddingSettlement();
                cas.SellerId = model.SellerId;
                cas.Date = model.Date;
                cas.Value = model.Value;
                cas.Notes = model.Notes;
                cas.BrancheId = model.BrancheId;
                cas.UserId = model.UserId;

                _UnitOfWork.SellerAddingSettlement.Add(cas);
                _UnitOfWork.Complete();

                return Ok(model);
            }
            return BadRequest("the model is not valid");

        }

        [HttpPut()]
        public IActionResult Edit([FromBody] SellerAddingSettlementDto model)
        {

            if (ModelState.IsValid)
            {

                var cas = _UnitOfWork.SellerAddingSettlement.GetById(model.Id);
                if (cas == null)
                {
                    return BadRequest("لم يتم ايجاد التسوية");
                }
                cas.SellerId = model.SellerId;
                cas.Date = model.Date;
                cas.Value = model.Value;
                cas.Notes = model.Notes;
                cas.BrancheId = model.BrancheId;
                cas.UserId = model.UserId;

                try
                {
                    _UnitOfWork.SellerAddingSettlement.Update(cas);
                    _UnitOfWork.Complete();
                    return Ok(model);
                }
                catch (Exception x)
                {

                    return BadRequest(x.Message);
                }
            }
            return BadRequest("the model is not valid");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var cas = _UnitOfWork.SellerAddingSettlement.GetById(id);

            if (cas == null)
            {
                return BadRequest("لم يتم ايجاد التسوية");
            }
            _UnitOfWork.SellerAddingSettlement.Delete(cas);
            _UnitOfWork.Complete();
            return Ok("تم الحذف");
        }

        private List<SellerAddingSettlementDto> ToDto(List<SellerAddingSettlement> source)
        {
            List<SellerAddingSettlementDto> data = new List<SellerAddingSettlementDto>();
            foreach (var item in source)
            {
                data.Add(new SellerAddingSettlementDto
                {
                    Id = item.Id,
                    Date = item.Date,
                    Notes = item.Notes,
                    SellerId = item.SellerId,
                    SellerName = item.Seller.Name,
                    BrancheId = item.BrancheId,
                    UserId = item.UserId,
                    Value = item.Value  
                    
                });
            }

            return data;
        }
    }
}
