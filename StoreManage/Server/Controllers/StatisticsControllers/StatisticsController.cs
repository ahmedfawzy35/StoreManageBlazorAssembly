using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Models;

namespace StoreManage.Server.Controllers.StatisticsControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<StatisticsController>


        [HttpGet]
        public async Task<IActionResult> GetDaySimpleStatistic([FromBody] DayDto date)
        {
            var result = await _unitOfWork.Statistics.GetDaySimpleStatisticForBranche(date);
            return Ok( result);
        }

    }
}
