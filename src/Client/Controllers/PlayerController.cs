using CitizenFX.Core;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class PlayerController : ClientAccessor
    {
        internal PlayerController (ClientMain client) : base (client)
        {
            Game.Player.DispatchsCops = false;
            Client.RegisterTickHandler(ClearWanted);
            Client.RegisterTickHandler(DisableShuffle);
        }

        private async Task ClearWanted()
        {
            if (Game.Player.WantedLevel > 0)
                Game.Player.WantedLevel = 0;

            await BaseScript.Delay(0);
        }

        private async Task DisableShuffle()
        {
            SetPedConfigFlag(PlayerPedId(), 184, true);
            if (!GetIsTaskActive(Game.PlayerPed.Handle, 164) && GetIsTaskActive(Game.PlayerPed.Handle, 165))
                SetPedIntoVehicle(Game.PlayerPed.Handle, Game.PlayerPed.CurrentVehicle.Handle, (int)VehicleSeat.Passenger);

            await BaseScript.Delay(0);
        }
    }
}
