using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.PurchaseDtos;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.PurchaseControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseBackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseBackController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<OrderController>


        [HttpGet]
        public IActionResult GetAll([FromBody] int brancheId)
        {
            return Ok(_unitOfWork.PurchaseBack.GetAllPurchasesBack(brancheId));
        }


        [HttpGet]
        public IActionResult GetAllForDate([FromBody] TimeDto time)
        {
            return Ok(_unitOfWork.PurchaseBack.GetAllForDate(time.DateFrom, time.BrancheId));
        }
        [HttpGet]
        public IActionResult GetAllForTime([FromBody] TimeDto time)
        {
            return Ok(_unitOfWork.PurchaseBack.GetAllForTime(time.DateFrom, time.DateTo, time.BrancheId));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetbYId(int id)
        {
            var myorder = _unitOfWork.PurchaseBack.GetPurchaseBack(id);
            if (myorder.Id != 0)
            {

                return Ok(myorder);
            }


            return NotFound("لم يتم ايجاد مرتجع المشتريات");
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PurchaseBackDto model)
        {
            if (ModelState.IsValid)
            {
                var myorder = new PurchaseBack();
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
                    myorder = await _unitOfWork.PurchaseBack.AddAsync(myorder);
                    _unitOfWork.Complete();

                    model.Id = myorder.Id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضاضفة مرتجع المشتريات ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut()]
        public IActionResult Edit([FromBody] PurchaseBackDto model)
        {


            if (ModelState.IsValid)
            {
                var myorder = _unitOfWork.PurchaseBack.GetById(model.Id);
                if (myorder == null)
                {
                    return BadRequest("لم يتم العثور على مرتجع المشتريات ");
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
                    _unitOfWork.PurchaseBack.Update(myorder);
                    _unitOfWork.Complete();

                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل مرتجع المشتريات");
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
                var myorder = _unitOfWork.PurchaseBack.GetById(id);
                if (myorder == null) return BadRequest("لم يتم ايجاد مرتجع المشتريات في قاعدة البيانات");
                _unitOfWork.PurchaseBack.Delete(myorder);
                _unitOfWork.Complete();
                return Ok("تم حذف مرتجع المشتريات ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف مرتجع االمشتريات  ");
            }
        }

    }
}
