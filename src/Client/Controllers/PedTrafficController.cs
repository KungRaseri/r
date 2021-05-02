using CitizenFX.Core;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    /// <summary>
    /// Handles local peds and traffic.
    /// </summary>
    public class PedTrafficController : ClientAccessor
    {
        internal PedTrafficController (ClientMain client) : base (client)
        {
            for (var i = 1; i <= 15; i++)
                EnableDispatchService(i, false);

            Client.RegisterTickHandler(GameTick);
        }

        private async Task GameTick()
        {
            ClearAreaOfCops(0f, 0f, 0f, 15000.0f, 1);
            await BaseScript.Delay(50);
        }
    }
}
