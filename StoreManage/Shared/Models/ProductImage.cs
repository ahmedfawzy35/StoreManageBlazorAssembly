using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Required]
        public byte[]? ImageData { get; set; } // محتوى الصورة
        public byte[]? Thumbnail { get; set; } 
        public string? ImageName { get; set; } // اسم الصورة الأصلي (اختياري)

        public string? ContentType { get; set; } // نوع الملف (image/png أو image/jpeg)

        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
