using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace OpenRP.Framework.Server.Controllers
{
    public class VehicleController : ServerAccessor
    {
        ConcurrentDictionary<int, bool> vehState;

        internal VehicleController(ServerMain server) : base(server)
        {
            vehState = new ConcurrentDictionary<int, bool>();
            Server.Event.RegisterEvent(ServerEvent.STORE_ENGINE_STATE, new Action<int, bool>(OnVehicleState));
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

            Server.Event.TriggerClientEvent(ClientEvent.STORE_ENGINE_STATE, JsonConvert.SerializeObject(vehState));
        }
    }
}
