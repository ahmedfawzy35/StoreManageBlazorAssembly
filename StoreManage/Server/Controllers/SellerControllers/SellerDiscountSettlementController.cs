using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato.CustomerSettlementDtos;
using StoreManage.Shared.Dtos.SellerDato.SellerSettlementDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.SellerControllers
{
    [Route("api/[controller]/[action]")]

    [ApiController]
    public class SellerDiscountSettlementController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;

        public SellerDiscountSettlementController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var include = new string[1];
            include[0] = "Seller";
            var cas = _UnitOfWork.SellerDiscountSettlement.FindAll(x => true, include);
            return Ok(ToDto(cas.ToList()));
        }
        [HttpGet]
        public IActionResult GetAllForBranche([FromBody] int brancheId)

        {
            var include = new string[1];
            include[0] = "Seller";
            var cas = _UnitOfWork.SellerDiscountSettlement.FindAll(x => x.BrancheId == brancheId, include);
            return Ok(ToDto(cas.ToList()));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sas = _UnitOfWork.SellerDiscountSettlement.GetById(id);
            if (sas != null)
            {
                var seller = _UnitOfWork.Seller.GetById(sas.SellerId);
                var sasDto = new SellerDiscountSettlementDto();
                sasDto.Id = sas.Id;
                sasDto.UserId = sas.UserId;
                sasDto.BrancheId = sas.BrancheId;
                sasDto.SellerId = sas.SellerId;
                sasDto.Date = sas.Date;
                sasDto.Value = sas.Value;
                sasDto.Notes = sas.Notes;
                sasDto.SellerName = seller == null ? " " : seller.Name;

                return Ok(sasDto);
            }
            else
            {
                return BadRequest("لم يتم ايجاد البيانات");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] SellerDiscountSettlementDto model)
        {
            if (ModelState.IsValid)
            {
                var cas = new SellerDiscountSettlement();
                cas.SellerId = model.SellerId;
                cas.Date = model.Date;
                cas.Value = model.Value;
                cas.Notes = model.Notes;
                cas.BrancheId = model.BrancheId;
                cas.UserId = model.UserId;

                _UnitOfWork.SellerDiscountSettlement.Add(cas);
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

                var cas = _UnitOfWork.SellerDiscountSettlement.GetById(model.Id);
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
                    _UnitOfWork.SellerDiscountSettlement.Update(cas);
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

            var cas = _UnitOfWork.SellerDiscountSettlement.GetById(id);

            if (cas == null)
            {
                return BadRequest("لم يتم ايجاد التسوية");
            }
            _UnitOfWork.SellerDiscountSettlement.Delete(cas);
            _UnitOfWork.Complete();
            return Ok("تم الحذف");
        }

        private List<SellerDiscountSettlementDto> ToDto(List<SellerDiscountSettlement> source)
        {
            List<SellerDiscountSettlementDto> data = new List<SellerDiscountSettlementDto>();
            foreach (var item in source)
            {
                data.Add(new SellerDiscountSettlementDto
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
