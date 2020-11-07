using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Domain.Models
{
    public class Product : BaseEntity
    {      
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductVariant> Variants { get; set; }
  
    }
}
