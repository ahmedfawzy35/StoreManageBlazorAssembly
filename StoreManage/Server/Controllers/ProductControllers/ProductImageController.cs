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

                _unitOfWork.ProductImage.Add(productImage);
            }

             _unitOfWork.Complete();
            return Ok("✅ تم رفع الصور بنجاح.");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAllImage()
        {
            _unitOfWork.ProductImage.DeleteRange(_unitOfWork.ProductImage.FindAll(x => true));
             _unitOfWork.Complete();
            return Ok("✅ تم حذف جميع الصور بنجاح.");
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
