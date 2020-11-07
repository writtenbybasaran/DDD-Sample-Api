using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Domain.Exceptions
{
    public class StockException : Exception
    {
        public StockException(string message) : base(message)
        {

        }
    }
}
