using System;
using System.Collections.Generic;
using System.Text;

namespace BasketService.Domain.Exceptions
{
    public class EfException : Exception
    {
        public EfException(string message) : base(message)
        {
        }
    }
}
