using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    class VehicleToggleSeatbelt : VehicleToggleComponent
    {
        bool _status;
        float[] speed;
        Vector3[] velocity;

        internal VehicleToggleSeatbelt()
        {
            _status = false;
            speed = new float[]{ 0, 0 };
            velocity = new Vector3[] { Vector3.Zero, Vector3.Zero };

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.RegisterTickHandler(PlayerMonitor);
            Client.RegisterTickHandler(VehicleMonitor);
        }

        private void ToggleComponent(dynamic args)
        {
            if (args.type == "belt")
                _status = args.status;

            var eventName = "SEATBELT_MONITOR";
            var data = new
            {
                eventName,
                _status
            };

            SendNuiMessage(JsonConvert.SerializeObject(data));
        }

        async Task PlayerMonitor()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                if (_status)
                {
                    DisableControlAction(0, (int)Control.VehicleExit, true);
                    DisableControlAction(27, (int)Control.VehicleExit, true);
                }
            }

            await BaseScript.Delay(0);
        }

        async Task VehicleMonitor()
        {
            if (!_status)
            {
                speed[1] = speed[0];
                speed[0] = TrackedVehicle.Speed;

                if (GetEntitySpeedVector(TrackedVehicle.Handle, true).Y > 1f && speed[0] >= 7.699028 && speed[1] - speed[0] > speed[0] * 0.255)
                {
                    var pos = Game.PlayerPed.Position;
                    var forward = Game.PlayerPed.ForwardVector;

                    pos.X += forward.X;
                    pos.Y += forward.Y;
                    pos.Z += -0.47f;

                    Game.PlayerPed.Position = pos;
                    Game.PlayerPed.Velocity = velocity[1];
                    await BaseScript.Delay(1);
                    Game.PlayerPed.Ragdoll(5000);
                }

                velocity[1] = velocity[0];
                velocity[0] = TrackedVehicle.Velocity;
            }

            await BaseScript.Delay(0);
        }
    }
}
