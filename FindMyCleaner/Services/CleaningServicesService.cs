using FindMyCleaner.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FindMyCleaner.Services
{
    public class CleaningServicesService
    {
        private readonly IMongoCollection<CleaningServices> _cleaningServices;

        public CleaningServicesService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _cleaningServices = mongoDatabase.GetCollection<CleaningServices>(
                mongoDbSettings.Value.CollectionName);
        }

        public async Task<List<CleaningServices>> GetAsync(string? suburb, string? serviceType, double? maxPrice)
        {
            var filter = Builders<CleaningServices>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(suburb))
            {
                filter &= Builders<CleaningServices>.Filter.Eq(service => service.suburb, suburb);
            }

            if (!string.IsNullOrWhiteSpace(serviceType))
            {
                filter &= Builders<CleaningServices>.Filter.Eq(service => service.serviceType, serviceType);
            }

            if (maxPrice.HasValue)
            {
                filter &= Builders<CleaningServices>.Filter.Lte(service => service.priceFrom, maxPrice.Value);
            }

            return await _cleaningServices.Find(filter).ToListAsync();
        }

        //GET by Id
        public async Task<CleaningServices?> GetByIdAsync(string id)
        {
            return await _cleaningServices
                .Find(service => service.Id == id)
                .FirstOrDefaultAsync();
        }

        //POST add data
        public async Task CreateAsync(CleaningServices newService)
        {
            await _cleaningServices.InsertOneAsync(newService);
        }

        //PUT update data 
        public async Task UpdateAsync(string id, CleaningServices updatedService)
        {
            await _cleaningServices.ReplaceOneAsync(service => service.Id == id, updatedService);
        }

        //DELETE to delete data
        public async Task RemoveAsync(string id)
        {
            await _cleaningServices.DeleteOneAsync(service => service.Id == id);
        }

        //keyword search
        public async Task<List<CleaningServices>> SearchByKeywordAsync(string keyword)
        {
            var filter = Builders<CleaningServices>.Filter.Or(
                Builders<CleaningServices>.Filter.Regex(service => service.serviceName, new MongoDB.Bson.BsonRegularExpression(keyword, "i")),
                Builders<CleaningServices>.Filter.Regex(service => service.serviceType, new MongoDB.Bson.BsonRegularExpression(keyword, "i")),
                Builders<CleaningServices>.Filter.Regex(service => service.suburb, new MongoDB.Bson.BsonRegularExpression(keyword, "i")),
                Builders<CleaningServices>.Filter.Regex(service => service.keywords, new MongoDB.Bson.BsonRegularExpression(keyword, "i"))
            );

            return await _cleaningServices.Find(filter).ToListAsync();
        }
        //Sorting data
        public async Task<List<CleaningServices>> GetSortedByPriceAsync(string order)
        {
            var sort = order.ToLower() == "desc"
                ? Builders<CleaningServices>.Sort.Descending(service => service.priceFrom)
                : Builders<CleaningServices>.Sort.Ascending(service => service.priceFrom);

            return await _cleaningServices.Find(_ => true).Sort(sort).ToListAsync();
        }

    }
}
