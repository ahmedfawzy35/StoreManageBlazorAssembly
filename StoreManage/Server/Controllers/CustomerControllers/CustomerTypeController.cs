using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato.CustomerSettlementDtos;
using StoreManage.Shared.Dtos.CustomerDato.CustomerTypeDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.CustomerControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CustomerTypeController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ct = _UnitOfWork.CustomerType.GetAll();
            return Ok(ct);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ct = _UnitOfWork.CustomerType.GetById(id);
            if (ct != null)
            {
               
                var ctDto = new CustomerTypeDto();
                ctDto.Id = ct.Id;
                ctDto.Name = ct.Name;
               

                return Ok(ctDto);
            }
            else
            {
                return BadRequest("لم يتم ايجاد البيانات");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CustomerTypeDto model)
        {
            if (ModelState.IsValid)
            {
                var ct= new CustomerType();
                ct.Name = model.Name;
              

                _UnitOfWork.CustomerType.Add(ct);
                _UnitOfWork.Complete();

                return Ok(model);
            }
            return BadRequest("the model is not valid");

        }

        [HttpPut()]
        public IActionResult Edit([FromBody] CustomerTypeDto model)
        {

            if (ModelState.IsValid)
            {

                var ct = _UnitOfWork.CustomerType.GetById(model.Id);
                if (ct == null)
                {
                    return BadRequest("لم يتم ايجاد نوع العميل");
                }
                ct.Name = model.Name;
              

                _UnitOfWork.CustomerType.Update(ct);
                _UnitOfWork.Complete();
                return Ok(model);
            }
            return BadRequest("the model is not valid");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var ct= _UnitOfWork.CustomerType.GetById(id);

            if (ct == null)
            {
                return BadRequest("لم يتم ايجاد نوع العميل");
            }
            _UnitOfWork.CustomerType.Delete(ct);
            _UnitOfWork.Complete();
            return Ok("تم الحذف");
        }
    }
}
