using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Domain.Models
{
    public class BasketItem : BaseEntity
    {     
        public Product Product { get; set; }
        public int Count { get; set; }
        
        public virtual Basket Basket { get; set; }
    }
}
