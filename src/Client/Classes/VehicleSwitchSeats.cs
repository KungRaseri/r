using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleSwitchSeats : VehicleToggleComponent
    {
        int _index;
        bool _status;

        internal VehicleSwitchSeats(int index)
        {
            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
        }

        async void ToggleComponent(dynamic args)
        {
            if (args.type == "seat")
            {
                SetPedIntoVehicle(Game.PlayerPed.Handle, Vehicle.Handle, args.index);
                await SeatTaken();
            }
        }
    }
}
