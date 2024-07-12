using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato.CustomerSettlementDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CustomerControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerDiscountSettlementController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CustomerDiscountSettlementController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var include = new string[1];
            include[0] = "Customer";
            var cas = _UnitOfWork.CustomerDiscountSettlement.FindAll(x=> true , include);
            return Ok(ToDto(cas.ToList()));
        }

        [HttpGet]
        public IActionResult GetAllForBranche([FromBody]int BrancheId)
        {
            var include = new string[1];
            include[0] = "Customer";
            var cas = _UnitOfWork.CustomerDiscountSettlement.FindAll(x => x.BrancheId == BrancheId, include);
            return Ok(ToDto(cas.ToList()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cas = _UnitOfWork.CustomerDiscountSettlement.GetById(id);
            if (cas != null)
            {
                var customer = _UnitOfWork.Customer.GetById(cas.CustomerId);
                var casDto = new CustomerAddingSettlementDto();
                casDto.Id = cas.Id;
                casDto.UserId = cas.UserId;
                casDto.BrancheId = cas.BrancheId;
                casDto.CustomerId = cas.CustomerId;
                casDto.Date = cas.Date;
                casDto.Value = cas.Value;
                casDto.Notes = cas.Notes;
                casDto.CustomerName = customer == null ? " " : customer.Name;

                return Ok(casDto);
            }
            else
            {
                return BadRequest("لم يتم ايجاد البيانات");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CustomerAddingSettlementDto model)
        {
            if (ModelState.IsValid)
            {
                var cas = new CustomerDiscountSettlement();
                cas.CustomerId = model.CustomerId;
                cas.Date = model.Date;
                cas.Value = model.Value;
                cas.Notes = model.Notes;
                cas.BrancheId = model.BrancheId;
                cas.UserId = model.UserId;

                _UnitOfWork.CustomerDiscountSettlement.Add(cas);
                _UnitOfWork.Complete();

                return Ok(model);
            }
            return BadRequest("the model is not valid");

        }

        [HttpPut()]
        public IActionResult Edit([FromBody] CustomerAddingSettlementDto model)
        {

            if (ModelState.IsValid)
            {

                var cas = _UnitOfWork.CustomerDiscountSettlement.GetById(model.Id);
                if (cas == null)
                {
                    return BadRequest("لم يتم ايجاد التسوية");
                }
                cas.CustomerId = model.CustomerId;
                cas.Date = model.Date;
                cas.Value = model.Value;
                cas.Notes = model.Notes;
                cas.BrancheId = model.BrancheId;
                cas.UserId = model.UserId;

                _UnitOfWork.CustomerDiscountSettlement.Update(cas);
                _UnitOfWork.Complete();
                return Ok(model);
            }
            return BadRequest("the model is not valid");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var cas = _UnitOfWork.CustomerDiscountSettlement.GetById(id);

            if (cas == null)
            {
                return BadRequest("لم يتم ايجاد التسوية");
            }
            _UnitOfWork.CustomerDiscountSettlement.Delete(cas);
            _UnitOfWork.Complete();
            return Ok("تم الحذف");
        }
        private List<CustomerDiscountSettlementDto> ToDto(List<CustomerDiscountSettlement> source)
        {
            List<CustomerDiscountSettlementDto> data = new List<CustomerDiscountSettlementDto>();
            foreach (var item in source)
            {
                data.Add(new CustomerDiscountSettlementDto
                {
                    Id = item.Id,
                    Date = item.Date,
                    Notes = item.Notes,
                    CustomerId = item.CustomerId,
                    CustomerName = item.Customer.Name,
                    BrancheId = item.BrancheId,
                    UserId = item.UserId,
                    Value = item.Value

                });
            }

            return data;
        }
    }
}
