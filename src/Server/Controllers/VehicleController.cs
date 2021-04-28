using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server.Controllers
{
    public class VehicleController : ServerAccessor
    {
        ConcurrentDictionary<int, bool> vehState;

        internal VehicleController (ServerMain server) : base (server)
        {
            vehState = new ConcurrentDictionary<int, bool>();
            Server.Event.RegisterEvent(ServerEvent.RECEIVE_VEHICLE_STATE, new Action<int, bool>(OnVehicleState));
        }

        async void OnVehicleState(int handle, bool engine)
        {
            if (!vehState.ContainsKey(handle))
            {
                while (!vehState.TryAdd(handle, engine))
                    await BaseScript.Delay(0);
            }
            else
            {
                vehState.TryUpdate(handle, engine, !engine);
            }

            foreach (var item in vehState)
                Debug.WriteLine($"Handle: {item.Key}, Engine: {item.Value}");

            Server.Event.TriggerClientEvent(ClientEvent.SEND_VEHILCE_STATE, JsonConvert.SerializeObject(vehState));
        }
    }
}
