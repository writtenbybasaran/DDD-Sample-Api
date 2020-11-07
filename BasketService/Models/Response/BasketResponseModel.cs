using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Models.Response
{
    public class BasketResponseModel
    {
        public Guid Id { get; set; }
        public List<BasketItemResponseModel> Items { get; set; }

    }
    public class BasketItemResponseModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } // Variantlar da eklenebilir detay ekstra uzamaması için burada bırakıyorum.
        public int Count { get; set; }

    }
}
