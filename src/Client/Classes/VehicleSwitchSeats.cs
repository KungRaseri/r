using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    /// <summary>
    /// Handles changing and monitoring of seats in vehicles.
    /// </summary>
    public class VehicleSwitchSeats : VehicleToggleComponent
    {
        int _total;
        int _lastTotal;

        internal VehicleSwitchSeats()
        {
            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_VEHICLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.RegisterTickHandler(ComponentMonitor);
        }

        async void ToggleComponent(dynamic args)
        {
            if (args.type == "seat")
            {
                SetPedIntoVehicle(Game.PlayerPed.Handle, TrackedVehicle.Handle, args.index);
                await SeatTaken();
            }
        }

        async Task ComponentMonitor()
        {
            _total = TotalTaken();

            if (_total != _lastTotal)
            {
                await SeatTaken();
                _lastTotal = _total;
            }

            await BaseScript.Delay(50);
        }

        private static int TotalTaken()
        {
            var tempTotal = 0;
            var temp = SeatStorage();

            foreach (var item in temp)
            {
                if (item)
                    tempTotal++;
            }

            return tempTotal;
        }
    }
}