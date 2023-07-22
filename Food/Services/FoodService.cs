using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Food.Models;
using Food.Databases;

namespace Food.Services
{
	public class FoodService
	{

        private readonly IMongoCollection<FoodModel> _foodCollection;


        public FoodService(IOptions<MongoConnection> mongoConnection)
		{
            var mongoClient = new MongoClient(mongoConnection.Value.Connection);
            var mongoDatabase = mongoClient.GetDatabase(mongoConnection.Value.DatabaseName);
            this._foodCollection = mongoDatabase.GetCollection<FoodModel>(mongoConnection.Value.CollectionName);
        }

        // GET
        public async Task<List<FoodModel>> Get()
        {
            return await this._foodCollection.Find(_ => true).ToListAsync();
        }

        // GET BY ID
        public async Task<FoodModel?> GetById(string id)
        {
            return await this._foodCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // POST
        public async Task Create(FoodModel foodModel)
        {
            await this._foodCollection.InsertOneAsync(foodModel);
        }

        // PATCH
        public async Task Patch(string id, FoodModel updateFoodModel)
        {
            await this._foodCollection.ReplaceOneAsync(x => x.Id == id, updateFoodModel);
        }

        // DELETE BY ID
        public async Task DeleteById(string id)
        {
            await this._foodCollection.DeleteOneAsync(x => x.Id == id);
        }
            
    }
}

