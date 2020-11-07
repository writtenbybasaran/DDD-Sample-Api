using BasketService.Domain.Exceptions;
using BasketService.Domain.Interfaces;
using BasketService.Extensions;
using BasketService.Models.Request;
using BasketService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IProductRepo _productRepo;
        public BasketService(IBasketRepo basketRepo, IProductRepo productRepo)
        {
            this._basketRepo = basketRepo;
            this._productRepo = productRepo;
        }
        public async Task<CreateBasketResponseModel> CreateBasket(CreateBasketRequestModel createBasketRequestModel)
        {

            try
            {
                var product = await _productRepo.GetById(createBasketRequestModel.ProductId);
                product.ProductControl();
                product.Variants.ProductVariantControl(createBasketRequestModel.ProductVariantId);
                product.Variants.StockControl(createBasketRequestModel.Count, createBasketRequestModel.ProductVariantId);

                var basket = new Domain.Models.Basket()
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Items = new List<Domain.Models.BasketItem>()
                    {
                        new Domain.Models.BasketItem()
                        {
                            Count = createBasketRequestModel.Count,
                            Id = Guid.NewGuid(),
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            IsDeleted = false,
                            Product = product
                        }
                    },
                    Id = Guid.NewGuid(),
                    ShoppingDate = null

                };
                await _basketRepo.Add(basket);
                #region Customer Check

                // TODO: Default Value success.
                #endregion
                return new CreateBasketResponseModel()
                {
                    BasketId = basket.Id,
                    IsSuccess = true
                };

            }catch(StockException stockExc)
            {
                return new CreateBasketResponseModel()
                {
                    ErrorMessage = stockExc.Message
                };
            }
            catch (Exception exc)
            {
                return new CreateBasketResponseModel()
                {
                    ErrorMessage = exc.Message
                };
            }

        }

        public async Task<AddBasketResponseModel> AddBasket(AddBasketRequestModel addBasketRequestModel)
        {

            try
            {
                var product = await _productRepo.GetById(addBasketRequestModel.ProductId);
                product.ProductControl();
                product.Variants.ProductVariantControl(addBasketRequestModel.ProductVariantId);
                product.Variants.StockControl(addBasketRequestModel.Count, addBasketRequestModel.ProductVariantId);
                
                var getBasket = await _basketRepo.GetById(addBasketRequestModel.BasketId);
                if (getBasket == null)
                    return new AddBasketResponseModel()
                    {
                        IsSuccess = false
                    };

                getBasket.Items.Add(new Domain.Models.BasketItem()
                {
                    Id = Guid.NewGuid(),
                    Count = addBasketRequestModel.Count,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    Product = product
                });

                var result = await _basketRepo.Update(getBasket);
                if (result)
                {
                    return new AddBasketResponseModel()
                    {
                        BasketId = getBasket.Id,
                        IsSuccess = true
                    };
                }
                else 
                    return new AddBasketResponseModel()
                    {
                        BasketId = getBasket.Id,
                        IsSuccess = false,
                        ErrorMessage = "Unhandled Error."
                    };
                                

            }
            catch (StockException stockExc)
            {
                return new AddBasketResponseModel()
                {
                    ErrorMessage = stockExc.Message
                };
            }
            catch (Exception exc)
            {
                return new AddBasketResponseModel()
                {
                    ErrorMessage = exc.Message
                };
            }

        }

        public async Task<List<BasketResponseModel>> GetBaskets()
        {

            return (await _basketRepo.GetAll()).Select(x => new BasketResponseModel()
            {
                Id = x.Id,
                Items = x.Items.Select(y => new BasketItemResponseModel()
                {
                    Id = y.Id,
                    Count = y.Count,
                    ProductName = y.Product.Name
                }).ToList()
            }).ToList();
        }

        public async Task<List<ProductResponseModel>> WellKnown()
        {
            return (await _productRepo.GetAll()).Select(x => new ProductResponseModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Variants = x.Variants.Select(y => new ProductVariantDetails()
                {
                    ProductVariantId=y.Id,
                    Variant = y.Variant
                }).ToList()
            }).ToList();
        }
    }
}
