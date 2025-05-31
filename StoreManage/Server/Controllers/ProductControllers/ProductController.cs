using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManage.Server.Data;
using StoreManage.Server.Servicies.Interfacies;
using StoreManage.Server.Servicies.Repositories;
using StoreManage.Shared.Dtos;
using StoreManage.Shared.Dtos.ProductDtos;
using StoreManage.Shared.Models;
using StoreManage.Shared.Utilitis.Extentions;

namespace StoreManage.Server.Controllers.ProductControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var include = new string[2];
            include[0] = "ProductImages";
            include[1] = "catogry";
            var products = _unitOfWork.Product.FindAll(x=>true, include);
           
            return Ok(products.ToList().ToProductSearchDto());
        }


        [HttpGet]
        public IActionResult GetAllShow()
        {
            var include = new string[2];
            include[0] = "ProductImages";
            include[1] = "catogry";
            var products = _unitOfWork.Product.FindAll(x => x.ShowInBill, include);
            
            return Ok(products.ToList().ToProductSearchDto());
        }












    }
}
