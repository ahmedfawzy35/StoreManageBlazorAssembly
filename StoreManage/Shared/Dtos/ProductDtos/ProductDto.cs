using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Barcode { get; set; } 

        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public int StartStock { get; set; } = 0;
        public double LastPurchasePrice { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public int LimitStock { get; set; }
        public int CatogryId { get; set; }
        public bool ShowInBill { get; set; }=true;
        public int BrancheId { get; set; }
        public string? CustomId { get; set; }
        public double Stock { get; set; }
        public DateTime? LastUpdate { get; set; }
        public  string? BrancheName { get; set; } = null!;
        public  string? CatogryName { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class ProductSearchDto
    {
        public int Id { get; set; }
        public string? Barcode { get; set; }

        public string Name { get; set; } = null!;
        public double LastPurchasePrice { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public string? CatogryName { get; set; } = null!;
        public ProductImageThumbnailDto? ThumbnailImageBase64 { get; set; }
    }

    public class ProductEditDto
    {
        public string Name { get; set; } = null!;
        public string? Details { get; set; }       
        public double LastPurchasePrice { get; set; }
        public double Price1 { get; set; }
        public double Price2 { get; set; }
        public bool ShowInBill { get; set; } = true;

    }  
    public class ProductImageThumbnailDto
    {
        public int Id { get; set; }
        [Required]
        public string? Thumbnail { get; set; }
        public string? ImageName { get; set; } // اسم الصورة الأصلي (اختياري)

        public string? ContentType { get; set; } // نوع الملف (image/png أو image/jpeg)

        public int ProductId { get; set; }


    }
    public class ProductImageDto
    {
        public int Id { get; set; }
        [Required]
        public string? ImageData { get; set; } // محتوى الصورة
        public string? ImageName { get; set; } // اسم الصورة الأصلي (اختياري)

        public string? ContentType { get; set; } // نوع الملف (image/png أو image/jpeg)

        public int ProductId { get; set; }


    }

}
