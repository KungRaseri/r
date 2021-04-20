using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRP.Framework.Server.Controllers
{
    public class EventController : ServerAccessor
    {
        ServerMain _server;

        internal EventController(ServerMain server) : base(server)
        {
            _server = server;
        }

        public void RegisterEvent(string name, MulticastDelegate callback)
        {
            _server.Events[name] += callback;
        }
    }
}
