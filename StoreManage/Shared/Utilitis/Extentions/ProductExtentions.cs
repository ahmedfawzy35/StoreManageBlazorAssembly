using StoreManage.Shared.Dtos.ProductDtos;
using StoreManage.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
                ThumbnailImageBase64 = p.ProductImages.FirstOrDefault() != null 
                      ? new ProductImageThumbnailDto
                      {
                          Id = p.ProductImages.FirstOrDefault().Id,
                          ImageName = p.ProductImages.FirstOrDefault().ImageName,
                          ContentType = p.ProductImages.FirstOrDefault().ContentType,
                          Thumbnail = p.ProductImages.FirstOrDefault().Thumbnail != null
                                        ? Convert.ToBase64String(p.ProductImages.FirstOrDefault().Thumbnail)
                                        : null,
                          ProductId = p.ProductImages.FirstOrDefault().ProductId


                      }
                      : null ,
            }).ToList();
        }
    }
}
/*new ProductImageThumbnailDto
{
    Id = p.ProductImages.FirstOrDefault().Id,
                          ImageName = p.ProductImages.FirstOrDefault().ImageName,
                          ContentType = p.ProductImages.FirstOrDefault().ContentType,
                          Thumbnail = p.ProductImages.FirstOrDefault().Thumbnail != null
                                        ? Convert.ToBase64String(p.ProductImages.FirstOrDefault().Thumbnail)
                                        : null,
                          ProductId = p.ProductImages.FirstOrDefault().ProductId


                      }*/