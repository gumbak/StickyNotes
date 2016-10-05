using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace StickyNotes
{      
    class Database
    {
        private string DATABASE_PATH = "stickyNotesDatabase";
        private string COLLECTION_PATH = "stickyNotesCollection";
        private IMongoClient _client;
        private IMongoDatabase _database;

        public Database()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(DATABASE_PATH);
        }

        public void save(string id, string content)
        {
            BsonDocument document = new BsonDocument
            {
                { "id", id },
                { "content", content }
            };

            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>(COLLECTION_PATH);
            collection.InsertOneAsync(document);
        }

        public async Task<string> get(string id, string fieldName)
        {
            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>(COLLECTION_PATH);
            var filter = Builders<BsonDocument>.Filter.Eq("id", id);
            var cursor = await collection.FindAsync(filter);
            while (await cursor.MoveNextAsync())
            {
                var document = cursor.Current.First();
                return document.GetValue(fieldName).AsString;
            }
            return null;
        }

        public void getAllNoteIds()
        {

        }
    }
}
