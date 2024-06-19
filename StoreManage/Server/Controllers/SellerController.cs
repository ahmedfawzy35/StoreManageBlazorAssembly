using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.SellerDato;

namespace StoreManage.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IUnitOfWork _seller;

        public SellerController(IUnitOfWork seller)
        {
            _seller = seller;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var cus = _seller.Seller.GetAll();
            return Ok(cus);
        }


        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cus = _seller.Seller.GetById(id);
            return Ok(cus);
        }
        // GET api/<CustomerController>/5
        [HttpGet]
        public async Task<IActionResult> Search([FromBody] string name)
        {
            var cus = await _seller.Seller.FindAllAsync(x => x.Name.Contains(name));
            return Ok(cus);
        }
        [HttpGet]
        public async Task<IActionResult> Account([FromBody] int id)
        {
            var customerAccount = await _seller.Seller.GetSellerAccount(id, DateTime.Now.AddYears(-2), DateTime.Now, false);
            return Ok(customerAccount);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Add([FromBody] SellerAddDto model)
        {
            if (ModelState.IsValid)
            {
                _seller.Seller.Add(model);
                _seller.Complete();

                return Ok(model);
            }
            return BadRequest("the model is not valid");

        }

        // PUT api/<CustomerController>/5
        [HttpPut()]
        public IActionResult Edit([FromBody] SellerAddDto model)
        {
            if (model.Id == 0)
            {
                return BadRequest("customer id not found");
            }
            if (ModelState.IsValid)
            {
                _seller.Seller.Edit(model);
                _seller.Complete();
                return Ok(model);
            }
            return BadRequest("the model is not valid");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cus = _seller.Seller.GetById(id);
            if (cus == null)
            {
                return BadRequest("customer not found");
            }
            _seller.Seller.Delete(cus);
            _seller.Complete();
            return Ok();
        }

    }
}
