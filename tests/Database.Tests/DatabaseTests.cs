﻿using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;
using Xunit;

namespace OpenRP.Framework.Tests
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
