using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.OrderDtos;
using StoreManage.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManage.Server.Controllers.OrderControllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderBackController : ControllerBase
    {
        private readonly IUnitOfWork _orderBack;

        public OrderBackController(IUnitOfWork orderBack)
        {
            _orderBack = orderBack;
        }
        // GET: api/<OrderController>


        [HttpGet]
        public IActionResult GetAll([FromBody] int brancheId)
        {
            return Ok(_orderBack.OrderBack.GetAllOrdersBack(brancheId));
        }


        [HttpGet]
        public IActionResult GetAllForDate([FromBody] TimeDto time)
        {
            return Ok(_orderBack.OrderBack.GetAllForDate(time.DateFrom, time.BrancheId));
        }
        [HttpGet]
        public IActionResult GetAllForTime([FromBody] TimeDto time)
        {
            return Ok(_orderBack.OrderBack.GetAllForTime(time.DateFrom, time.DateTo, time.BrancheId));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetbYId(int id)
        {
            var myorder = _orderBack.OrderBack.GetOrderBack(id);
            if (myorder.Id != 0)
            {

                return Ok(myorder);
            }


            return NotFound("لم يتم ايجاد فاتورة المرتجع");
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderDto model)
        {
            if (ModelState.IsValid)
            {
                var myorder = new OrderBack();
                myorder.Date = model.Date;
                myorder.CustomerId = model.CustomerId;
                myorder.Total = model.Total;
                myorder.Paid = model.Paid;
                myorder.Discount = model.Discount;
                myorder.RemainingAmount = model.RemainingAmount;
                myorder.BrancheId = model.BrancheId;
                myorder.OrderProfit = model.OrderProfit;
                myorder.OrderNumber = model.OrderNumber;
                myorder.Notes = model.Notes;
                try
                {
                    myorder = await _orderBack.OrderBack.AddAsync(myorder);
                    _orderBack.Complete();
                    model.Id = myorder.Id;
                    
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضاضفة فاتورة المرتجع ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut()]
        public IActionResult Edit( [FromBody] OrderDto model)
        {


            if (ModelState.IsValid)
            {
                var myorder = _orderBack.OrderBack.GetById(model.Id);
                if (myorder == null)
                {
                    return BadRequest("لم يتم العثور على فاتورة المرتجع ");
                }

                myorder.Date = model.Date;
                myorder.CustomerId = model.CustomerId;
                myorder.Total = model.Total;
                myorder.Paid = model.Paid;
                myorder.Discount = model.Discount;
                myorder.RemainingAmount = model.RemainingAmount;
                myorder.BrancheId = model.BrancheId;
                myorder.OrderProfit = model.OrderProfit;
                myorder.OrderNumber = model.OrderNumber;
                myorder.Notes = model.Notes;
                try
                {
                    _orderBack.OrderBack.Update(myorder);
                    _orderBack.Complete();
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل فاتورة المرتجع");
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
                var myorder = _orderBack.OrderBack.GetById(id);
                if (myorder == null) return BadRequest("لم يتم ايجاد فاتورة المرتجع في قاعدة البيانات");
                _orderBack.OrderBack.Delete(myorder);
                _orderBack.Complete();
                return Ok("تم حذف فاتورة المرتجع ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف فاتورة المرتجع ");
            }
        }


    }
}
