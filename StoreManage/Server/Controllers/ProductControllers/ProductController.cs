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

        [HttpGet]
        public IActionResult GetAllShowUpdatedAfter([FromQuery] DateTime date)
        {
            var include = new string[2];
            include[0] = "ProductImages";
            include[1] = "catogry";
            var products = _unitOfWork.Product.FindAll(x => x.ShowInBill && x.LastUpdate > date, include);

            return Ok(products.ToList().ToProductSearchDto());
        }
        [HttpPost()]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("❌ اسم المنتج مطلوب.");

            var product = new Product
            {
                Barcode = string.IsNullOrWhiteSpace(dto.Barcode) ? Guid.NewGuid().ToString() : dto.Barcode,
                Name = dto.Name,
                Details = dto.Details,
                StartStock = dto.StartStock,
                Stock = dto.StartStock, // ممكن تغيرها حسب منطقك
                LastPurchasePrice = dto.LastPurchasePrice,
                Price1 = dto.Price1,
                Price2 = dto.Price2,
                LimitStock = dto.LimitStock,
                CatogryId = dto.CatogryId,
                ShowInBill = dto.ShowInBill,
                BrancheId = dto.BrancheId,
                CustomId = dto.CustomId,
                LastUpdate = DateTime.Now
            };

            await _unitOfWork.Product.AddAsync(product);
             _unitOfWork.Complete();

            return Ok(new { message = "✅ تم إنشاء المنتج بنجاح", product.Id });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShowInBill(int id)
        {
            var product = await _unitOfWork.Product.FindAsync(p => p.Id == id);
            if (product == null)
                return NotFound("❌ المنتج غير موجود.");

            product.ShowInBill = false;
            product.LastUpdate = DateTime.Now;
            _unitOfWork.Product.Update(product);
             _unitOfWork.Complete();

            return Ok("✅ تم تعديل ShowInBill إلى false.");
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateShowInBillForMultiple([FromBody] List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
                return BadRequest("❌ يجب إرسال قائمة بمعرفات المنتجات.");

            var products = await _unitOfWork.Product.FindAllAsync(p => productIds.Contains(p.Id));

            foreach (var product in products)
            {
                product.ShowInBill = false;
                product.LastUpdate = DateTime.Now;

                _unitOfWork.Product.Update(product);
            }

             _unitOfWork.Complete();
            return Ok($"✅ تم تعديل خاصية ShowInBill لعدد {products.Count()} منتج.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBasicProduct(int id, [FromBody] ProductEditDto dto)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
                return NotFound($"🚫 المنتج برقم {id} غير موجود");

            // تعديل الخصائص المسموح بها فقط
            product.Name = dto.Name;
            product.Details = dto.Details;
            product.LastPurchasePrice = dto.LastPurchasePrice;
            product.Price1 = dto.Price1;
            product.Price2 = dto.Price2;
            product.ShowInBill = dto.ShowInBill;

            product.LastUpdate = DateTime.Now.Date;

            _unitOfWork.Product.Update(product);
             _unitOfWork.Complete();

            return Ok("✅ تم تعديل بيانات المنتج بنجاح");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.FindAsync(p => p.Id == id);
            if (product == null)
                return NotFound("❌ المنتج غير موجود.");

           
            _unitOfWork.Product.Delete(product);
            _unitOfWork.Complete();

            return Ok($"✅ تم حذف المنتج  {product.Name}   بنجاح .");
        }



        [HttpDelete()]
        public async Task<IActionResult> DeleteProductForMultiple([FromBody] List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
                return BadRequest("❌ يجب إرسال قائمة بمعرفات المنتجات.");

            var products = await _unitOfWork.Product.FindAllAsync(p => productIds.Contains(p.Id));

            foreach (var product in products)
            {
                product.ShowInBill = false;
                _unitOfWork.Product.DeleteRange(products);
            }

            _unitOfWork.Complete();
            return Ok($"✅ تم حذف عدد  {products.Count()} منتج بنجاح.");
        }


    }
}
