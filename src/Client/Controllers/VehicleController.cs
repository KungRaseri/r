using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class VehicleController : ClientAccessor
    {
        Vehicle _vehicle;

        bool _engine;
        bool _lastEngine;



        internal VehicleController (ClientMain client) : base (client)
        {
            _vehicle = new Vehicle(0);

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_ENGINE, new Action<dynamic>(ToggleEngine));

            Client.RegisterKeyBinding("ToggleVehiclePanel", "(HUD) Vehicle Panel", "grave", new Action(ToggleVehiclePanel));
            Client.RegisterTickHandler(VehicleMonitor);
        }

        private void ToggleEngine(dynamic args)
        {
            _vehicle.IsEngineRunning = args.engine;
        }

        private async Task VehicleMonitor()
        {
            _engine = _vehicle.IsEngineRunning;

            if (_engine != _lastEngine)
            {
                _engine = _vehicle.IsEngineRunning;
                _lastEngine = _engine;
                SendData();
            }

            BaseScript.Delay(50);
        }

        private void ToggleVehiclePanel()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                _vehicle = Game.PlayerPed.CurrentVehicle;
                var eventName = "TOGGLE_VEHICLE_PANEL_MODULE";
                UIElement.ToggleNuiModule(eventName, true, true);
                SendData();
            }
        }

        private void SendData()
        {
            string eventName = "VEHICLE_PANEL_DATA";
            var data = new
            {
                eventName,
                _engine
            };
            Debug.WriteLine(_engine.ToString());
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }
    }
}
