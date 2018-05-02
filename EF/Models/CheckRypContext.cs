using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EF.Models
{
    public class CheckRypContext
    {
        IMongoDatabase database; // база данных
        IGridFSBucket gridFS;   // файловое хранилище

        public CheckRypContext()
        {
            // строка подключения
            string connectionString = "mongodb://localhost:27017/ryps2";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            database = client.GetDatabase(connection.DatabaseName);
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
        }
        // обращаемся к коллекции Ryps
        private IMongoCollection<CheckedRyp> Ryps
        {
            get { return database.GetCollection<CheckedRyp>("CheckedRyps2"); }
        }
        // получаем все документы, используя критерии фальтрации
        public async Task<IEnumerable<CheckedRyp>> GetRyps()
        {
            // строитель фильтров
            var builder = new FilterDefinitionBuilder<CheckedRyp>();
            var filter = builder.Empty; // фильтр для выборки всех документов

            return await Ryps.Find(filter).ToListAsync();
        }

        // получаем один документ по id
        public async Task<CheckedRyp> GetRyp(string id)
        {
            return await Ryps.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        // добавление документа
        public async Task Create(CheckedRyp p)
        {
            await Ryps.InsertOneAsync(p);
        }
        // обновление документа
        public async Task Update(CheckedRyp p)
        {
            //await Ryps.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.MongoId)), p);
            await Ryps.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(p.Id)), p);
        }
        // удаление документа
        public async Task Remove(string id)
        {
            await Ryps.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
    }
}