using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class EventController : ClientAccessor
    {
        ClientMain _client;

        internal EventController (ClientMain client) : base (client)
        {
            _client = client;
        }

        public void RegisterNuiEvent(string name, MulticastDelegate callback)
        {
            RegisterNuiCallbackType(name);
            var nuiName = $"__cfx_nui:{name}";
            _client.Events[nuiName] += callback;
        }
    }
}
