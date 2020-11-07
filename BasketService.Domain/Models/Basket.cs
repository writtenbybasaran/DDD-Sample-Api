using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Domain.Models
{
    public class Basket : BaseEntity
    {     
        public List<BasketItem> Items { get; set; }
        public DateTimeOffset? ShoppingDate { get; set; }
    }
}
