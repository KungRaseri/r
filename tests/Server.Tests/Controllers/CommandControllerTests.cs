using System;
using System.Collections.Generic;
using OpenRP.Framework.Server.Controllers;
using Xunit;

namespace OpenRP.Framework.Tests.Controllers
{
    public class CommandControllerTests
    {
        [Fact]
        public void RegisterTest()
        {
            var CommandController = new CommandController(null);
            CommandController.Register("test", new Action<int>(i => { }), "test", new List<string>());
            Assert.Throws<FormatException>(() => CommandController.Register("/test", new Action<int>(i => { }), "test", new List<string>()));
        }

        [Fact]
        public void UnregisterTest()
        {
            var CommandController = new CommandController(null);
            CommandController.Register("test", new Action<int>(i => { }), "test", new List<string>());
            CommandController.Unregister("test");

            CommandController = new CommandController(null);
            Assert.Throws<IndexOutOfRangeException>(() => CommandController.Unregister("test"));
        }
    }
}