using MongoDB.Driver;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;
using Xunit;

namespace OpenRP.Framework.Tests
{
    public class DatabaseTests
    {
        private readonly IMongoClient _client;
        private const string DatabaseName = "openrp";

        public DatabaseTests()
        {
            _client = new MongoClient("mongodb://localhost:27017");
        }

        [Fact]
        public async void testing_mongostuff()
        {
            var test = new DocumentRepository<Account>(_client.GetDatabase(DatabaseName));

            Assert.False(test.Exists);
        }
    }
}
