using MongoDB.Driver;
using OpenRP.Framework.Database.Document;

namespace OpenRP.Framework.Database
{
    public class DatabaseContext
    {
        private readonly IMongoClient _client;
        private IMongoDatabase _db;

        public DocumentRepository<Account> Accounts;

        public DatabaseContext(string connString, string database)
        {
            _client = new MongoClient(connString);
            _db = _client.GetDatabase(database);

            InitializeRepositories();
        }

        private void InitializeRepositories()
        {
            Accounts = new DocumentRepository<Account>(_db);
        }
    }
}
