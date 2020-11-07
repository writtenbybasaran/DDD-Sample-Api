using BasketService.Domain.Models;
using BasketService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.DataSeed
{
    public class DataGenerator
    {
        internal static MongoDbRunner _runner;
        internal static IMongoCollection<Product> _productCollection;
        internal static string _databaseName = "development";
        internal static string _collectionName = "Product";
        public static void Initialize(IServiceProvider serviceProvider)
        {


            CreateConnection();

            _productCollection.InsertOne(new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPhone",
                Description = "IPhone Cep Telefonu",
                IsActive = true,
                IsDeleted = false,
                Variants = new List<ProductVariant>()
                        {
                            new ProductVariant()
                            {
                                Id=Guid.NewGuid(),
                                Variant= "64 GB",
                                StockCount = 6,
                                IsActive = false,
                                IsDeleted = false
                            },new ProductVariant()
                            {
                                Id=Guid.NewGuid(),
                                Variant= "128 GB",
                                StockCount = 6,
                                IsActive = true,
                                IsDeleted = false
                            },new ProductVariant()
                            {
                                Id=Guid.NewGuid(),
                                Variant= "256 GB",
                                StockCount = 3,
                                IsActive = true,
                                IsDeleted = false
                            }
                        },
                CreatedDate = DateTime.Now
            });
            _productCollection.InsertOne(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Gömlek",
                Description = "Erkek gömlek",
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Variants = new List<ProductVariant>()
                        {
                            new ProductVariant()
                            {
                                Id=Guid.NewGuid(),
                                Variant= "S",
                                StockCount = 3,
                                IsActive = true,
                                IsDeleted = false,                                
                            },new ProductVariant()
                            {
                                Id=Guid.NewGuid(),
                                Variant= "M",
                                StockCount = 0,
                                IsActive = true,
                                IsDeleted = false,                               
                            },new ProductVariant()
                            {
                                Id=Guid.NewGuid(),
                                Variant= "L",
                                StockCount = 3,
                                IsActive = false,
                                IsDeleted = true,
                            }
                        }
            });

            var list =  _productCollection.Find(Builders<Product>.Filter.Empty).ToList();



            ///
            /// If u want to use relational db, use flowing code
            ///
            //using (var context = new BasketDbContext(
            //    serviceProvider.GetRequiredService<DbContextOptions<BasketDbContext>>()))
            //{

            //    if (context.Products.Any())
            //    {
            //        return;
            //    }

            //    context.SaveChanges();
            //}
        }

        internal static void CreateConnection()
        {
            _runner = MongoDbRunner.Start(singleNodeReplSet: false);

            MongoClient client = new MongoClient("mongodb://127.0.0.1:27038/");
            IMongoDatabase database = client.GetDatabase(_databaseName);
            _productCollection = database.GetCollection<Product>(_collectionName);
        }
    }
}
