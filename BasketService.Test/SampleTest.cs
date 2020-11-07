using BasketService.Domain.Interfaces;
using BasketService.Infrastructure.Repositories;
using BasketService.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BasketService.Test
{
    public class SampleTest
    {

        private IServiceCollection _services;
        private IServiceProvider _serviceProvider;
        private IBasketService _basketService;

        public SampleTest()
        {
            _services = new ServiceCollection();
            _services.AddScoped<IBasketService, BasketService.Services.BasketService>();
            _serviceProvider = _services.BuildServiceProvider();
            _basketService = _serviceProvider.GetRequiredService<IBasketService>();
        }

        [Fact]
        public async void CreateBasket_Success()
        {
            BuildData.Initialize();

            var items = await _basketService.WellKnown();
            
            
            Assert.True((await _basketService.CreateBasket(new Models.Request.CreateBasketRequestModel()
            {
                Count = 1,
                CustomerId = 1,
                ProductId = items.First().Id,
                ProductVariantId = items.First().Variants.First().ProductVariantId
            })).IsSuccess);

        }

       [Fact]
        public async void CreateBasket_Error()
        {
            BuildData.Initialize();

            var items = await _basketService.WellKnown();


            Assert.True((await _basketService.CreateBasket(new Models.Request.CreateBasketRequestModel()
            {
                Count = 100,
                CustomerId = 1,
                ProductId = items.First().Id,
                ProductVariantId = items.First().Variants.First().ProductVariantId
            })).IsSuccess);

        }


    }
}
