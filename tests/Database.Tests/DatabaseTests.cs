using MongoDB.Driver;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;
using Xunit;

namespace OpenRP.Framework.Tests
{
    public class DatabaseTests
    {
        private const string connString = "mongodb://localhost:27017";
        private const string database = "openrp";
        private DatabaseContext _context;

        public DatabaseTests()
        {
        }

        [Fact]
        public async void testing_mongostuff()
        {
            _context = new DatabaseContext(connString, database);

            var target = _context.Accounts;

            await target.Post(new Account()
            {
                
            })

            Assert.False(target.Exists);
        }
    }
}
