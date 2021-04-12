using MongoDB.Driver;

namespace OpenRP.Framework.Database
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class DocumentRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly MongoClient _mongoClient;

        /// <summary>
        /// 
        /// </summary>
        public DocumentRepository()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = _mongoClient.GetDatabase("open-rp");

        }
    }
}
