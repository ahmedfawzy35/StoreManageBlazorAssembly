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
            var cas = _UnitOfWork.CustomerDiscountSettlement.GetAll();
            return Ok(cas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cas = _UnitOfWork.CustomerDiscountSettlement.GetById(id);
            if (cas != null)
            {
                var customer = _UnitOfWork.Customer.GetById(cas.CustomerId);
                var casDto = new CustomerDiscountSettlementDto();
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
        public IActionResult Add([FromBody] CustomerDiscountSettlementDto model)
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
        public IActionResult Edit([FromBody] CustomerDiscountSettlementDto model)
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
    }
}
