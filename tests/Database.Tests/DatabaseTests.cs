using System.Linq;
using MongoDB.Bson;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;
using Xunit;

namespace OpenRP.Framework.Tests
{
    public class DatabaseContext_AccountTests
    {
        private const string connString = "mongodb://localhost:27017";
        private const string database = "openrp";

        private readonly DatabaseContext _context;
        private readonly DocumentRepository<Account> _sut; // System Under Test

        public DatabaseContext_AccountTests()
        {
            _context = new DatabaseContext(connString, database);
            _sut = _context.Accounts;
        }

        [Fact(Skip = "Should be ran locally")]
        public async void PostingNewAccount_and_GettingAccounts_ReturnsList_WithNewAccount()
        {
            //arrange 
            var account = new Account()
            {
                Identifiers = new[] { "steam:123456", "license:123456" },
                Roles = new[] { "Development", "Staff" }
            };

            //act
            await _sut.PostAsync(account);

            var results = (await _sut.GetAsync()).ToList();

            //assert
            Assert.True(results.Any());
        }

        [Fact(Skip = "Should be ran locally")]
        public async void FindingAccount_ReturnsAccount()
        {
            //arrange
            var identifier = "steam:123456";

            //act
            var result = await _sut.FindAsync(identifier);

            //assert
            Assert.Contains(identifier, result.Identifiers);
        }

        [Fact(Skip = "Should be ran locally")]
        public async void PatchingAccount_IsAcknowledged()
        {
            //arrange
            var identifier = "steam:123456";

            //act
            var result = await _sut.PatchAsync(new Account()
            {
                Id = ObjectId.Parse("6084ddd1254b79cf9f7abf1a"),
                Identifiers = new[] { "test" },
                Roles = new[] { "test" }
            });

            //assert
            Assert.True(result);
        }

    }
}
