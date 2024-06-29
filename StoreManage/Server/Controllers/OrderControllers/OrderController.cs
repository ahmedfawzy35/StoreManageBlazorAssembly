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
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _order;

        public OrderController(IUnitOfWork order)
        {
            _order = order;
        }
        // GET: api/<OrderController>


        [HttpGet]
        public IActionResult GetAll([FromBody] int brancheId)
        {
            return Ok(_order.Order.GetAllOrders(brancheId));
        }


        [HttpGet]
        public IActionResult GetAllForDate([FromBody] TimeDto time)
        {
            return Ok(_order.Order.GetAllForDate(time.DateFrom, time.BrancheId));
        }
        [HttpGet]
        public IActionResult GetAllForTime([FromBody] TimeDto time)
        {
            return Ok(_order.Order.GetAllForTime(time.DateFrom, time.DateTo, time.BrancheId));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetbYId(int id)
        {
            var myorder = _order.Order.GetOrder(id);
            if (myorder.Id != 0)
            {

                return Ok(myorder);
            }


            return NotFound("لم يتم ايجاد الفاتورة");
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderDto model)
        {
            if (ModelState.IsValid)
            {
                var myorder = new Order();
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
                    myorder = await _order.Order.AddAsync(myorder);
                    model.Id = myorder.Id;
                    _order.Complete();
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest($"لم يتم اضاضفة الفاتورة ");
                }

            }
            else
            {
                return BadRequest("البيانات غير مكتملة");
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] OrderDto model)
        {


            if (ModelState.IsValid)
            {
                var myorder = _order.Order.GetById(id);
                if (myorder == null)
                {
                    return BadRequest("لم يتم العثور على الفاتورة ");
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
                    _order.Order.Update(myorder);
                    _order.Complete();
                    model.Id = id;
                    return Ok(model);
                }
                catch (Exception)
                {

                    return BadRequest("فشل تعديل الفاتورة");
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
                var myorder = _order.Order.GetById(id);
                if (myorder == null) return BadRequest("لم يتم ايجاد الفاتورة في قاعدة البيانات");
                _order.Order.Delete(myorder);
                _order.Complete();
                return Ok("تم حذف الفاتورة ");
            }
            catch (Exception)
            {

                return BadRequest("لم يتم حذف الفاتورة ");
            }
        }


    }
}
