using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// Handles server-side Discord integration
    /// </summary>
    public class DiscordController : ServerAccessor
    {
        internal DiscordController(ServerMain server) : base(server)
        {
        }
    }
}
