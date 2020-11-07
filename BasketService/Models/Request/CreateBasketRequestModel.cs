using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Models.Request
{
    public class CreateBasketRequestModel
    {
        
        public Guid ProductId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Count { get; set; }
        public int CustomerId { get; set; }
    }
}
