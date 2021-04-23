using CitizenFX.Core;
using System.Threading.Tasks;

namespace OpenRP.Framework.Client.Controllers
{
    public class PlayerController : ClientAccessor
    {
        internal PlayerController (ClientMain client) : base (client)
        {
            Game.Player.DispatchsCops = false;
            Client.RegisterTickHandler(GameTick);
        }

        private async Task GameTick()
        {
            if (Game.Player.WantedLevel > 0)
            {
                Game.Player.WantedLevel = 0;
            }

            await BaseScript.Delay(50);
        }
    }
}
