using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreManage.Server.Controllers
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
        [HttpGet]
        public IActionResult  GetAll()
        {
            var cus = _customer.Customer.GetAll();
            return Ok(cus);
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
        public async Task< IActionResult> Search([FromBody] string name)
        {
            var cus = await _customer.Customer.FindAllAsync(x=>x.Name.Contains(name));
            return Ok(cus);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Add([FromBody] Customer model)
        {
            if (ModelState.IsValid)
            {
                _customer.Customer.Add(model);
                var result = _customer.Complete();
                if (result > 0)
                {
                    return Ok("add successfuly !!");
                }
                else
                {
                    return BadRequest("faild to add");
                }
            }
            else
                return BadRequest("faild to add");

        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
