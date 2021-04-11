using System;
using System.Collections.Generic;
using System.Text;
using Database;
using Database.Document;
using Xunit;

namespace Server.Tests
{
    public class DatabaseTests
    {
        public DatabaseTests()
        {

        }

        [Fact]
        public async void testing_mongostuff()
        {
            var test = new DocumentRepository<Account>();
        }
    }
}
