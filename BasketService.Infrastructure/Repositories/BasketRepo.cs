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
    public class BasketRepo : IBasketRepo
    {
        private readonly BasketDbContext _context;
        internal MongoDbRunner _runner;
        internal IMongoCollection<Basket> _basketCollection;
        internal string _databaseName = "development";
        internal string _collectionName = "Basket";

        public BasketRepo(string connectionString)
        {
            CreateConnection(connectionString);
        }
        public async Task Add(Basket entity)
        {
            try
            {
                _basketCollection.InsertOne(entity);
            }
            catch (Exception)
            {

                throw new EfException("Insert exception.");
            } 
        }
        public async Task<Basket> GetById(Guid id)
        {
            var filterBuilder = new FilterDefinitionBuilder<Basket>();
            var filter = filterBuilder.Eq(x => x.Id, id);
            return await _basketCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            
        }

        public async Task<IEnumerable<Basket>> GetAll()
        {
            return await _basketCollection.Find(Builders<Basket>.Filter.Empty).ToListAsync();
            
        }

        public async Task<bool> Remove(Basket entity)
        {
            var filterBuilder = new FilterDefinitionBuilder<Basket>();
            var filter = filterBuilder.Eq(x => x.Id, entity.Id);
            var result = _basketCollection.DeleteOne(filter);
            if (result.DeletedCount > 0) return true;
            else return false;
       
        }

        public async Task<bool> Update(Basket entity)
        {
            var filterBuilder = new FilterDefinitionBuilder<Basket>();
            var filter = filterBuilder.Eq(x => x.Id, entity.Id);
            var result = _basketCollection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);
            if (result.ModifiedCount > 0) return true;
            else return false;
          
        }

        internal void CreateConnection(string connectionString)
        {
            _runner = MongoDbRunner.Start(singleNodeReplSet: false);

            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(_databaseName);
            _basketCollection = database.GetCollection<Basket>(_collectionName);
        }
    }
}
