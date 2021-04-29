using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static OpenRP.Framework.Client.Classes.VehicleToggleComponent;

namespace OpenRP.Framework.Client.Controllers
{
    public class VehicleController : ClientAccessor
    {
        internal VehicleController (ClientMain client) : base (client)
        {
            VehicleToggleComponent.Client = client;

            new VehicleEngineToggle();

            new VehicleToggleDoor(0);
            new VehicleToggleDoor(1);
            new VehicleToggleDoor(2);
            new VehicleToggleDoor(3);
            new VehicleToggleDoor(4);
            new VehicleToggleDoor(5);

            new VehicleToggleWindow(0);
            new VehicleToggleWindow(1);
            new VehicleToggleWindow(2);
            new VehicleToggleWindow(3);

            VehicleToggleComponent.Vehicle = new Vehicle(0);
            Seat = -1;
            Taken = new bool[] { false, false, false, false };

            Client.RegisterKeyBinding("ToggleVehiclePanel", "(HUD) Vehicle Panel", "grave", new Action(ToggleVehiclePanel));
        }

        private void ToggleVehiclePanel()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                VehicleToggleComponent.Vehicle = Game.PlayerPed.CurrentVehicle;
                var eventName = "TOGGLE_VEHICLE_PANEL_MODULE";
                UIElement.ToggleNuiModule(eventName, true, true);
                SendData();
            }
        }
    }
}
