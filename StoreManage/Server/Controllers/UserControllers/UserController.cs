using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos.CustomerDato;
using StoreManage.Shared.Dtos.UserDtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.UserControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWork _user;

        public UserController(IUnitOfWork user)
        {
            _user = user;
        }
        [HttpGet]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var login = await _user.User.LoginAsync(model);

                return Ok(login);
            }
            return BadRequest("البيانات غير صحيحه");

        }
    }
}
