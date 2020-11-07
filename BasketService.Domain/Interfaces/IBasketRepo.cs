using BasketService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Domain.Interfaces
{
    public interface IBasketRepo
    {
        Task Add(Basket entity);
        Task<bool> Update(Basket entity);
        Task<bool> Remove(Basket entity);
        Task<IEnumerable<Basket>> GetAll();
        Task<Basket> GetById(Guid id);
    }
}
