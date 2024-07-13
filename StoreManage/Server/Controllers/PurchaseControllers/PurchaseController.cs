using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.PurchaseDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.PurchaseControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<OrderController>


        [HttpGet]
        public IActionResult GetAll([FromBody] int brancheId)
        {
            return Ok(_unitOfWork.Purchase.GetAllPurchases(brancheId));
        }


        [HttpGet]
        public IActionResult GetAllForDate([FromBody] TimeDto time)
        {
            return Ok(_unitOfWork.Purchase.GetAllForDate(time.DateFrom, time.BrancheId));
        }
        [HttpGet]
        public IActionResult GetAllForTime([FromBody] TimeDto time)
        {
            return Ok(_unitOfWork.Purchase.GetAllForTime(time.DateFrom, time.DateTo, time.BrancheId));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetbYId(int id)
        {
            var myorder = _unitOfWork.Purchase.GetPurchase(id);
            if (myorder.Id != 0)
            {

                return Ok(myorder);
            }


            return NotFound("لم يتم ايجاد فاتورة الشراء");
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PurchaseDto model)
        {
            if (ModelState.IsValid)
            {
                var myorder = new Purchase();
                myorder.Date = model.Date;
                myorder.SellerId = model.SellerId;
                myorder.Total = model.Total;
                myorder.Paid = model.Paid;
                myorder.Discount = model.Discount;
                myorder.RemainingAmount = model.RemainingAmount;
                myorder.BrancheId = model.BrancheId;
                myorder.OrderNumber = model.OrderNumber;
                myorder.Notes = model.Notes;
                try
                {
                    myorder = await _unitOfWork.Purchase.AddAsync(myorder);
                    _unitOfWork.Complete();

                    model.Id = myorder.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضاضفة فاتورة الشراء ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut()]
        public IActionResult Edit([FromBody] PurchaseDto model)
        {


            if (ModelState.IsValid)
            {
                var myorder = _unitOfWork.Purchase.GetById(model.Id);
                if (myorder == null)
                {
                    return BadRequest("لم يتم العثور على فاتورة الشراء ");
                }

                myorder.Date = model.Date;
                myorder.SellerId = model.SellerId;
                myorder.Total = model.Total;
                myorder.Paid = model.Paid;
                myorder.Discount = model.Discount;
                myorder.RemainingAmount = model.RemainingAmount;
                myorder.BrancheId = model.BrancheId;
                myorder.OrderNumber = model.OrderNumber;
                myorder.Notes = model.Notes;
                try
                {
                    _unitOfWork.Purchase.Update(myorder);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل فاتورة الشراء");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var myorder = _unitOfWork.Purchase.GetById(id);
                if (myorder == null) return BadRequest("لم يتم ايجاد فاتورة الشراء في قاعدة البيانات");
                _unitOfWork.Purchase.Delete(myorder);
                _unitOfWork.Complete();
                return Ok("تم حذف فاتورة الشراء ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف فاتورة الشراء ");
            }
        }

    }
}
