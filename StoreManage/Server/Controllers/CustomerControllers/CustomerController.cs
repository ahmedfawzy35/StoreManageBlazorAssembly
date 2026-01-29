using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;
using StoreManage.Shared.Utilitis.Extentions;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManage.Server.Controllers.CustomerControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _customer;

        public CustomerController(IUnitOfWork customer)
        {
            _customer = customer;
        }
        // GET: api/<CustomerController>
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var cus = _customer.Customer.GetAll();
        //    return Ok(cus);
        //}
        [HttpGet]
        public IActionResult GetAll([FromBody] GetCustometDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _customer.User.Find(x => x.Id == model.UserId);
            if (user == null) return BadRequest(ModelState);
            if (!user.Enabled) return BadRequest("user is unenable");

            var include = new string[2];
            include[0] = "Branche";
            include[1] = "Customertype";
            var cus = _customer.Customer.FindAll(x=> !x.Archived);
            return Ok(cus.ToList().ToCustomerDto());
        }

        [HttpGet("{branchId}")]
        public IActionResult GetAllForBranche(int brancheId)
        {
            var include = new string[2];
            include[0] = "Branche";
            include[1] = "Customertype";
            var cus = _customer.Customer.FindAll(x => x.BrancheId == brancheId & !x.Archived, include);
            return Ok(cus.ToList().ToCustomerDto());
        }
        // GET that accepts dates as strings and parses them (safer for querystring formats)
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerOrdersForBranche([FromQuery] int brancheId, [FromQuery] string? dateFrom, [FromQuery] string? dateTo)
        {
            if (string.IsNullOrWhiteSpace(dateFrom) || string.IsNullOrWhiteSpace(dateTo))
                return BadRequest("Missing dateFrom or dateTo. Use query params: ?brancheId=1&dateFrom=yyyy-MM-dd&dateTo=yyyy-MM-dd");

            if (!DateTime.TryParse(dateFrom, out var from))
                return BadRequest($"Invalid dateFrom: '{dateFrom}'. Use ISO (yyyy-MM-dd) or an ISO timestamp.");

            if (!DateTime.TryParse(dateTo, out var to))
                return BadRequest($"Invalid dateTo: '{dateTo}'. Use ISO (yyyy-MM-dd) or an ISO timestamp.");

            try
            {
                var result = await _customer.Customer.GetAllCustomersOrderAsync(brancheId, from, to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // لا تكشف استثناءات حساسة في الإنتاج — فقط للديباغ الآن
                return StatusCode(500, new { message = "Server error", detail = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllForBranche2([FromBody] GetCustometDto model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _customer.User.Find(x => x.Id == model.UserId);
            if (user == null) return BadRequest(ModelState);
            if (!user.Enabled) return BadRequest("user is unenable");

            var include = new string[2];
            include[0] = "Branche";
            include[1] = "Customertype";
            var cus = _customer.Customer.FindAll(x => x.BrancheId == model.BrancheId & !x.Archived, include);
            return Ok(cus.ToList().ToCustomerDto());
        }
        [HttpGet("{branchId}")]
        public IActionResult GetAllForBrancheWithAccounts(int branchId)
        {
            var include =new string[2];
            include[0] = "Branche";
            include[1] = "Customertype";
            var cus = _customer.Customer.FindAll(x=>x.BrancheId == branchId & !x.Archived, include);
            return Ok(cus.ToList().ToCustomerDto());
        }


        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cus = _customer.Customer.GetById(id);
            return Ok(cus);
        }
        // GET api/<CustomerController>/5
        [HttpGet]
        public async Task<IActionResult> Search([FromBody] string name)
        {
            var cus = await _customer.Customer.FindAllAsync(x => x.Name.Contains(name) & !x.Archived);
            return Ok(cus);
        }
        [HttpGet]
        public async Task<IActionResult> Account([FromBody] int id)
        {
            var customerAccount = await _customer.Customer.GetCustomerAccount(id, DateTime.Now.AddYears(-2), DateTime.Now, false);
            return Ok(customerAccount);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Add([FromBody] CustomerAddDto model)
        {
            if (ModelState.IsValid)
            {
                _customer.Customer.Add(model);
                _customer.Complete();

                return Ok(model);
            }
            return BadRequest("the model is not valid");

        }

        // PUT api/<CustomerController>/5
        [HttpPut()]
        public IActionResult Edit([FromBody] CustomerAddDto model)
        {
            if (model.Id == 0)
            {
                return BadRequest("customer id not found");
            }
            if (ModelState.IsValid)
            {
                _customer.Customer.Edit(model);
                _customer.Complete();
                return Ok(model);
            }
            return BadRequest("the model is not valid");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cus = _customer.Customer.GetById(id);
            if (cus == null)
            {
                return BadRequest("customer not found");
            }
            _customer.Customer.Delete(cus);
            _customer.Complete();
            return Ok();
        }

        //private List<CustomerAddDto> _toCustomerDto(this List<Customer> source)
        //{
        //    List<custdt>
        //}
    }
}
