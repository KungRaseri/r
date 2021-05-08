using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// Handles game client vehicle data that must be synced with the server.
    /// </summary>
    public class VehicleController : ServerAccessor
    {
        ConcurrentDictionary<int, bool> _vehState;

        internal VehicleController(ServerMain server) : base(server)
        {
            _vehState = new ConcurrentDictionary<int, bool>();
            Server.Event.RegisterEvent(ServerEvent.STORE_ENGINE_STATE, new Action<int, bool>(OnVehicleState));
        }

        async void OnVehicleState(int handle, bool engine)
        {
            if (!_vehState.ContainsKey(handle))
            {
                while (!_vehState.TryAdd(handle, engine))
                    await BaseScript.Delay(0);
            }
            else
            {
                _vehState.TryUpdate(handle, engine, !engine);
            }

            Server.Event.TriggerClientEvent(ClientEvent.STORE_ENGINE_STATE, JsonConvert.SerializeObject(_vehState));
        }
    }
}
