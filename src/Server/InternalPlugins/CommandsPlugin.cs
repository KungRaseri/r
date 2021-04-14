using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRP.Framework.Server.InternalPlugins
{
    class CommandsPlugin : ServerAccessor
    {
        private readonly ServerMain _server;

        public CommandsPlugin(ServerMain server) : base(server)
        {
            _server = server;
        }
    }
}
