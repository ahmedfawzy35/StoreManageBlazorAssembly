using Microsoft.AspNetCore.Mvc;
using StoreManage.Shared.Models;
using StoreManage.Server.Servicies.Interfacies;

using SixLabors.ImageSharp.Formats.Jpeg;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
namespace StoreManage.Server.Controllers.ProductControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ رفع صورة واحدة
        [HttpPost]
        public async Task<IActionResult> UploadSingleImage([FromForm] IFormFile image, [FromForm] int productId)
        {
            if (image == null || image.Length == 0)
                return BadRequest("الصورة غير موجودة.");

            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            ms.Position = 0;
            var compressed = CompressImage(ms);
            var thumbnail = GenerateThumbnail(ms); // توليد الصورة المصغرة

            var productImage = new ProductImage
            {
                ProductId = productId,
                ImageData = compressed,
                Thumbnail = thumbnail,
                ImageName = image.FileName,
                ContentType = "image/jpeg"
            };

            _unitOfWork.ProductImage.Add(productImage);
            var product = await _unitOfWork.Product.GetByIdAsync(productId);
            if (product != null)
            {
                product.LastUpdate = DateTime.Now   ;

                _unitOfWork.Product.Update(product);
            }
             _unitOfWork.Complete();

            return Ok("✅ تم رفع الصورة بنجاح.");
        }

        // ✅ رفع عدة صور
        [HttpPost]
        public async Task<IActionResult> UploadMultipleImages([FromForm] List<IFormFile> images, [FromForm] int productId)
        {
            if (images == null || !images.Any())
                return BadRequest("الصور غير موجودة.");

            foreach (var image in images)
            {
                using var ms = new MemoryStream();
                await image.CopyToAsync(ms);
                ms.Position = 0;
                var compressed = CompressImage(ms);
                var thumbnail = GenerateThumbnail(ms); // توليد الصورة المصغرة
                var productImage = new ProductImage
                {
                    ProductId = productId,
                    ImageData = compressed,
                    Thumbnail = thumbnail, // توليد الصورة المصغرة
                    ImageName = image.FileName,
                    ContentType = "image/jpeg"
                };
                var product = await _unitOfWork.Product.GetByIdAsync(productId);
                if (product != null)
                {
                    product.LastUpdate = DateTime.Now;

                    _unitOfWork.Product.Update(product);
                }
                _unitOfWork.ProductImage.Add(productImage);
            }

             _unitOfWork.Complete();
            return Ok("✅ تم رفع الصور بنجاح.");
        }

        // ✅ حذف جميع  الصور
        [HttpPost]
        public async Task<IActionResult> DeleteAllImage()
        {
            _unitOfWork.ProductImage.DeleteRange(_unitOfWork.ProductImage.FindAll(x => true));
             _unitOfWork.Complete();
            return Ok("✅ تم حذف جميع الصور بنجاح.");
        }

        // ✅ حذف صورة واحدة
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _unitOfWork.ProductImage.GetByIdAsync(id);
            if (image == null)
            {
                return NotFound(new { message = $"No image found with ID = {id}" });
            }

            _unitOfWork.ProductImage.Delete(image);

            var product = await _unitOfWork.Product.GetByIdAsync(image.ProductId);
            if (product != null)
            {
                product.LastUpdate = DateTime.Now;

                _unitOfWork.Product.Update(product);
            }
            _unitOfWork.Complete();

            return Ok(new { message = $"Image with ID = {id} deleted successfully." });
        }

        // ✅ حذف عدة صور
        [HttpDelete()]
        public async Task<IActionResult> DeleteMultipleImages([FromBody] List<int> imageIds)
        {
            if (imageIds == null || !imageIds.Any())
                return BadRequest("يجب إرسال قائمة من معرفات الصور.");

            // نجيب الصور اللي الـ ID بتاعها في القائمة
            var images = await _unitOfWork.ProductImage
                .FindAllAsync(img => imageIds.Contains(img.Id));

            if (!images.Any())
                return NotFound("لم يتم العثور على أي صور بالمُعرفات المُرسلة.");

            foreach (var image in images)
            {
                _unitOfWork.ProductImage.Delete(image);
            }

             _unitOfWork.Complete();

            return Ok(new { message = "✅ تم حذف الصور المحددة بنجاح." });
        }


        // ✅جلب الصور الكبيرة
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductImagesBase64(int productId)
        {
            // الحصول على كل الصور المرتبطة بالمنتج
            var images = await _unitOfWork.ProductImage
                .FindAllAsync(img => img.ProductId == productId);

            if (images == null || !images.Any())
                return NotFound("🚫 لا توجد صور لهذا المنتج.");

            // تحويل كل صورة إلى Base64
            var result = images.Select(img => new
            {
                img.Id,
                img.ImageName,
                Base64 = $"data:{img.ContentType};base64,{Convert.ToBase64String(img.ImageData)}"
            });

            return Ok(result);
        }

        // ✅ حذف صور منتج
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImages(int id)
        {
          

            // نجيب الصور اللي الـ ID بتاعها في القائمة
            var images = await _unitOfWork.ProductImage
                .FindAllAsync(img => img.ProductId == id);

            if (!images.Any())
                return NotFound("لم يتم العثور على أي صور خاصة بالمنتج .");

            
                _unitOfWork.ProductImage.DeleteRange(images);
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product != null)
            {
                product.LastUpdate = DateTime.Now;

                _unitOfWork.Product.Update(product);
            }

            _unitOfWork.Complete();

            return Ok(new { message = "✅ تم حذف الصور المحددة بنجاح." });
        }


        public byte[] CompressImage(Stream inputStream)
        {
            using (var image = Image.Load(inputStream))  // دي طريقة التحميل الصحيحة في ImageSharp
            {
                using (var ms = new MemoryStream())
                {
                    // قلل الجودة مثلا لـ 75% عشان تضغط الصورة
                    image.Save(ms, new JpegEncoder { Quality = 75 });
                    return ms.ToArray();
                }
            }
        }
        private byte[] GenerateThumbnail(Stream inputStream)
        {
            // تأكد إن الستريم في بدايته
            inputStream.Position = 0;

            using var image = Image.Load(inputStream);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(100, 100),
                Mode = ResizeMode.Max
            }));

            using var ms = new MemoryStream();
            image.SaveAsJpeg(ms); // أو SaveAsPng لو حابب
            return ms.ToArray();
        }

    }
}
