using System;
using System.Collections.Generic;
using System.Text;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;
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
