using StoreManage.Shared.Dtos.ProductDtos;
using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManage.Shared.Utilitis.Extentions
{
    public static class ProductExtentions
    {



        public static List<ProductSearchDto> ToProductSearchDto(this List<Product> products)
        {

          
            return products.Select(p => new ProductSearchDto
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                CatogryName = p.catogry.Name,
                LastPurchasePrice = p.LastPurchasePrice,
                Price1 = p.Price1,
                Price2 = p.Price2,
                ImageBase64 = p.ProductImages.FirstOrDefault() != null 
                      ? Convert.ToBase64String(p.ProductImages.First().Thumbnail )
                      : null ,
            }).ToList();
        }
    }
}
