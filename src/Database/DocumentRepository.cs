using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using OpenRP.Framework.Common.Interface;

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

            Initialize();
        }

        private void Initialize()
        {

        }

        public async Task<IEnumerable<TDocument>> GetAsync()
        {
            var result = await _collection.FindAsync(FilterDefinition<TDocument>.Empty);

            return result.Current;
        }

        public async Task PostAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task<bool> PatchAsync(TDocument document)
        {
            var result = await _collection.UpdateOneAsync(FilterDefinition<TDocument>.Empty, new BsonDocumentUpdateDefinition<TDocument>(document.ToBsonDocument()));

            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(TDocument document)
        {
            var result = await _collection.DeleteOneAsync(FilterDefinition<TDocument>.Empty);

            return result.IsAcknowledged;
        }
    }
}
