using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Models.Response
{
    public class ProductResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductVariantDetails> Variants { get; set; }
    }

    public class ProductVariantDetails
    {
        public Guid ProductVariantId { get; set; }
        public string Variant { get; set; } 
       
    }
        
    
}
