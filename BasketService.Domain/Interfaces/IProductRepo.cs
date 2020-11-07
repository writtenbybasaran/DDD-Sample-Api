using BasketService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Domain.Interfaces
{
    public interface IProductRepo
    {
        Task Add(Product entity);
        Task<bool> Update(Product entity);
        Task<bool> Remove(Product entity);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
    }
}
