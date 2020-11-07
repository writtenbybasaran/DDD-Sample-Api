using BasketService.Domain.Exceptions;
using BasketService.Domain.Interfaces;
using BasketService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Mongo2Go;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Infrastructure.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly BasketDbContext _context;
        internal MongoDbRunner _runner;
        internal IMongoCollection<Product> _productCollection;
        internal string _databaseName = "development";
        internal string _collectionName = "Product";

        public ProductRepo(string connectionString)
        {
            CreateConnection(connectionString);
        }
        public async Task Add(Product entity)
        {
            try
            {
                _productCollection.InsertOne(entity);
            }
            catch (Exception)
            {

                throw new EfException("Insert exception.");
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {            
            return await _productCollection.Find(Builders<Product>.Filter.Empty).ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            var filter = filterBuilder.Eq(x => x.Id, id);
            return await _productCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        }

        public async Task<bool> Remove(Product entity)
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            var filter = filterBuilder.Eq(x => x.Id, entity.Id);
            var result = _productCollection.DeleteOne(filter);
            if (result.DeletedCount > 0) return true;
            else return false;
        }

        public async Task<bool> Update(Product entity)
        {
            var filterBuilder = new FilterDefinitionBuilder<Product>();
            var filter = filterBuilder.Eq(x => x.Id, entity.Id);
            var result = _productCollection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);
            if (result.ModifiedCount > 0) return true;
            else return false;
        }

        internal void CreateConnection(string connectionString)
        {
            _runner = MongoDbRunner.Start(singleNodeReplSet: false);

            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(_databaseName);
            _productCollection = database.GetCollection<Product>(_collectionName);
        }
    }
}
