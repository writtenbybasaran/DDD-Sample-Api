using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BasketService.Domain.Models
{
    public class ProductVariant : BaseEntity
    {  
        public string Variant { get; set; } // S - M - L - XL         35-36-37-38 etc..
        public int StockCount { get; set; }          
       
    }
}
