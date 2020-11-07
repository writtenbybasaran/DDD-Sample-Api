using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketService.Models.Request;
using BasketService.Models.Response;
using BasketService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            this._basketService = basketService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<List<BasketResponseModel>> GetBasket()
        {
            return await _basketService.GetBaskets();
        }


        [HttpPost]
        [Route("Create")]
        public async Task<CreateBasketResponseModel> CreateBasket([FromBody]CreateBasketRequestModel createBasketRequestModel)
        {
            return await _basketService.CreateBasket(createBasketRequestModel);
        }


        [HttpPut]
        [Route("Add")]
        public async Task<AddBasketResponseModel> AddBasket([FromBody]AddBasketRequestModel addBasketRequestModel)
        {
            return await _basketService.AddBasket(addBasketRequestModel);
        }



        /// <summary>
        /// This function is created only to receive data for testing purposes. It will not be used.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("WellKnown")]
        public async Task<List<ProductResponseModel>> ProductWellKnown()
        {
            return await _basketService.WellKnown();
        }

    }
}
