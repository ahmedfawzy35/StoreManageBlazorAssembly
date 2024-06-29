using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Models;
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
        [HttpGet]
        public IActionResult GetAll()
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
        public async Task<IActionResult> Search([FromBody] string name)
        {
            var cus = await _customer.Customer.FindAllAsync(x => x.Name.Contains(name));
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


    }
}
