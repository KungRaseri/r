using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace OpenRP.Framework.Database
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class DocumentRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        private readonly string _entityName = typeof(TDocument).Name.ToLower();

        /// <summary>
        /// 
        /// </summary>
        public DocumentRepository(IMongoDatabase db)
        {
            _collection = db.GetCollection<TDocument>(_entityName);
        }

        public async Task<IEnumerable<TDocument>> GetAsync()
        {
            var result = await _collection.FindAsync(FilterDefinition<TDocument>.Empty);

            return await result.ToListAsync();
        }

        public async Task<TDocument> FindAsync(string identifier)
        {
            var filter = Builders<TDocument>.Filter.AnyEq("Identifiers", identifier);

            var result = await (await _collection.FindAsync(filter)).ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task PostAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task<bool> PatchAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", document.Id);

            var result = await _collection.UpdateOneAsync(filter, new BsonDocumentUpdateDefinition<TDocument>(document.ToBsonDocument()));

            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", document.Id);

            var result = await _collection.DeleteOneAsync(filter);

            return result.IsAcknowledged;
        }
    }
}
