using System;
using System.Collections.Generic;
using Xunit;

namespace OpenRP.Framework.Server.Controllers.Tests
{
    public class CommandControllerTests
    {
        [Fact]
        public void RegisterTest()
        {
            var CommandController = new CommandController(null);
            CommandController.Register("test", new Action<int>(i => { }), "help", new List<string>());
            Assert.Single(CommandController.Commands);
            CommandController.Register("/test", new Action<int>(i => { }), "kelp", new List<string>());
            Assert.Single(CommandController.Commands);
            Assert.Equal("help", CommandController.Commands["test"].Help);
        }

        [Fact]
        public void UnregisterTest()
        {
            var CommandController = new CommandController(null);
            CommandController.Register("test", new Action<int>(i => { }), "help", new List<string>());
            CommandController.Unregister("test");
            Assert.Empty(CommandController.Commands);

            CommandController = new CommandController(null);
            CommandController.Unregister("test");
            Assert.Empty(CommandController.Commands);
        }
    }
}