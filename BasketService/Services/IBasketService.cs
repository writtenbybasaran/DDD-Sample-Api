using BasketService.Models.Request;
using BasketService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Services
{
    public interface IBasketService
    {
        Task<List<BasketResponseModel>> GetBaskets();
        /// <summary>
        /// Create Basket
        /// </summary>
        /// <returns>CreateBasketResponseModel</returns>
        Task<CreateBasketResponseModel> CreateBasket(CreateBasketRequestModel createBasketRequestModel);
        /// <summary>
        /// Add Basket Item
        /// </summary>
        /// <param name="addBasketRequestModel"></param>
        /// <returns></returns>
        Task<AddBasketResponseModel> AddBasket(AddBasketRequestModel addBasketRequestModel);
        Task<List<ProductResponseModel>> WellKnown();
    }
}
