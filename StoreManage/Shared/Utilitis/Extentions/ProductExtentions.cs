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
                Name = p.Name!,
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
        public static List<ProductDto> ToProductDto(this List<Product> products)
        {


            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name!,
                Barcode = p.Barcode,
                CatogryName = p.catogry.Name,
                LastPurchasePrice = p.LastPurchasePrice,
                Price1 = p.Price1,
                Price2 = p.Price2,
                BrancheId = p.BrancheId,
                BrancheName ="",
                Details = p.Details,
                StartStock = p.StartStock,
                LimitStock = p.LimitStock,
                ShowInBill = p.ShowInBill,
                CustomId = p.CustomId,
                Stock = p.Stock,
                CatogryId = p.CatogryId,
                ImageBase64 = p.ProductImages.FirstOrDefault() != null
                      ? Convert.ToBase64String(p.ProductImages.FirstOrDefault().Thumbnail)
                      : null,

              
            }).ToList();
        }
        public static ProductDto ToProductDto(this Product product)
        {


            return  new ProductDto
            {
                Id = product.Id,
                Name = product.Name!,
                Barcode = product.Barcode,
                CatogryName = product.catogry.Name,
                LastPurchasePrice = product.LastPurchasePrice,
                Price1 = product.Price1,
                Price2 = product.Price2,
                BrancheId = product.BrancheId,
                BrancheName = "",
                Details = product.Details,
                StartStock = product.StartStock,
                LimitStock = product.LimitStock,
                ShowInBill = product.ShowInBill,
                CustomId = product.CustomId,
                Stock = product.Stock,
                CatogryId = product.CatogryId,
                ImageBase64 = product.ProductImages.FirstOrDefault() != null
                      ? Convert.ToBase64String(product.ProductImages.FirstOrDefault().Thumbnail)
                      : null,


            };
        }
    }
}


