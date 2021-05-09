using CitizenFX.Core;
using OpenRP.Framework.Client.Classes;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    /// <summary>
    /// Handles the player character.
    /// </summary>
    public class PlayerController : ClientAccessor
    {
        bool _paused;
        bool _lastPaused;

        internal PlayerController (ClientMain client) : base (client)
        {
            Game.Player.DispatchsCops = false;
            SetMaxWantedLevel(0);
            DisplayRadar(false);
            Client.RegisterTickHandler(ClearWanted);
            Client.RegisterTickHandler(DisableShuffle);
            Client.RegisterTickHandler(PauseMonitor);
            Client.RegisterTickHandler(HideHud);
        }

        private async Task HideHud()
        {
            for (var i = 1; i < 23; i++)
                HideHudComponentThisFrame(i);
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

        async Task PauseMonitor()
        {
            _paused = IsPauseMenuActive();

            if (_paused != _lastPaused)
            {
                UIElement.ToggleNuiModule("GET_PAUSED_STATUS", !_paused);
                _lastPaused = _paused;
            }

            await BaseScript.Delay(50);
        }
    }
}
