using BasketService.Domain.Exceptions;
using BasketService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Extensions
{
    public static class ValidationExtension
    {
        public static bool StockControl(this List<ProductVariant> productVariants,int count,Guid variantId)
        {
            var productVariant = productVariants.FirstOrDefault(x => x.Id == variantId);
            if (productVariant.StockCount < count)
                throw new StockException("Stock Count Error");

            return true;
        }

        public static bool ProductControl(this Product product)
        {
            if (product.IsActive==false || product.IsDeleted==true)
                throw new StockException("Product Error");

            return true;
        }

        public static bool ProductVariantControl(this List<ProductVariant> productVariants,Guid variantId)
        {
            var productVariant = productVariants.FirstOrDefault(x => x.Id == variantId);
            if (productVariant == null || productVariant.IsActive == false || productVariant.IsDeleted == true)
                throw new Exception("Product Variant Error");
            return true;
        }

    }
}
