using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleEngineToggle : VehicleToggleComponent
    {
        bool _status;

        internal VehicleEngineToggle()
        {
            _status = false;
            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.RegisterTickHandler(VehicleStateMonitor);
        }

        private async Task VehicleStateMonitor()
        {
            if (Vehicle.IsEngineRunning != _status)
                SetVehicleEngineOn(Vehicle.Handle, _status, true, true);

            await BaseScript.Delay(0);
        }

        void ToggleComponent(dynamic args)
        {
            if (args.type == "engine")
            {
                _status = args.status;
                Vehicle.IsEngineRunning = _status;
            }
        }

        private async void SeatChange(VehicleSeat seat, bool status)
        {
            if (Vehicle.IsSeatFree(seat) && status)
            {
                SetPedIntoVehicle(Game.PlayerPed.Handle, Vehicle.Handle, (int)seat);

                Seat = -2;
                while (Seat < -1)
                {
                    Seat = (int)Game.PlayerPed.SeatIndex;
                    await BaseScript.Delay(50);
                }
            }
        }
    }
}
