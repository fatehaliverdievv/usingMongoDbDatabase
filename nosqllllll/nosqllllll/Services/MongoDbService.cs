using MongoDB.Bson;
using MongoDB.Driver;
using nosqllllll.Models;

namespace nosqllllll.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Telebe> _telebeler;

        public MongoDbService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QrupDb"); 
            _telebeler = database.GetCollection<Telebe>("Telebeler");
        }

        public List<Telebe> Get() => _telebeler.Find(_ => true).ToList();

        public Telebe Get(ObjectId id) => _telebeler.Find(x => x.Id == id).FirstOrDefault();

        public void Create(Telebe user) => _telebeler.InsertOne(user);
        public void Update(Telebe telebe)
        {
            var filter = Builders<Telebe>.Filter.Eq(t => t.Id, telebe.Id);
            _telebeler.ReplaceOne(filter, telebe);
        }
        public void Delete(ObjectId id)
        {
            var filter = Builders<Telebe>.Filter.Eq(t => t.Id, id);
            _telebeler.DeleteOne(filter);
        }
        public List<Telebe> GetFiltered(FilterDefinition<Telebe> filter)
        {
            return _telebeler.Find(filter).ToList();
        }
    }
}
