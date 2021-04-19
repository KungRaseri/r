using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IMongoCollection<TDocument> _mongoCollection;

        private readonly string _entityName = typeof(TDocument).Name.ToLower();

        public bool Exists { get; }

        /// <summary>
        /// 
        /// </summary>
        public DocumentRepository(IMongoDatabase db)
        {
            _mongoCollection = db.GetCollection<TDocument>(_entityName);

            Initialize();
        }

        private void Initialize()
        {
            if (Exists) return;

        }

        public async Task<IEnumerable<TDocument>> Get()
        {
            var result = await _mongoCollection.FindAsync(FilterDefinition<TDocument>.Empty);
            return result.Current;
        }
    }
}
