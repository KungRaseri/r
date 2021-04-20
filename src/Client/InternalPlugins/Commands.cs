using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Controllers;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.InternalPlugins
{
    /// <summary>
    /// Commands internal plugin.
    /// </summary>
    public class Commands : ClientAccessor
    {
        internal Commands (ClientMain client) : base (client)
        {
            Client.Event.RegisterEvent(ClientEvent.COMMAND_TP, new Action<string>(OnTpCommand));
        }

        private async void OnTpCommand(string args)
        {
            var list = JsonConvert.DeserializeObject<List<string>>(args);
            var length = list.Count;

            foreach (var item in list)
            {
                try
                {
                    float.Parse(item);
                }
                catch (FormatException e)
                {
                    ChatController.AddMessage(255, 0, 0, "Invalid arguments.");
                    return;
                }
            }

            if (length == 0)
            {
                if (!DoesBlipExist(GetFirstBlipInfoId(8)))
                    ChatController.AddMessage(255, 0, 0, "Waypoint not set.");
                else
                {
                    var gps = GetFirstBlipInfoId(8);
                    var pos = GetBlipCoords(gps);
                    await Teleport(pos);
                }
            }
            else if (length == 1)
            {
                var found = GetPlayerFromServerId(int.Parse(list[0]));
                if (found == -1)
                    ChatController.AddMessage(255, 0, 0, "Player does not exist.");
                else
                {
                    var ped = GetPlayerPed(found);
                    var pos = GetEntityCoords(ped, true);
                    await Teleport(pos);
                }
            }
            else if (length == 2)
            {
                var pos = Vector3.Zero;
                pos.X = float.Parse(list[0]);
                pos.Y = float.Parse(list[1]);
                await Teleport(pos);
            }
            else
                ChatController.AddMessage(255, 0, 0, "Invalid number of arguments.");
        }

        private static async Task Teleport(Vector3 pos)
        {
            var ground = 950f;

            for (var zz = 950f; zz >= 0f; zz -= 25f)
            {
                var z = zz;

                if (zz % 2 != 0)
                {
                    z = 950f - zz;
                }

                RequestCollisionAtCoord(pos.X, pos.Y, z);
                NewLoadSceneStart(pos.X, pos.Y, z, pos.X, pos.Y, z, 50f, 0);

                int timer = GetGameTimer();

                while (IsNetworkLoadingScene())
                {
                    if (GetGameTimer() - timer > 1000)
                        break;
                    await BaseScript.Delay(0);
                }

                SetEntityCoords(Game.PlayerPed.Handle, pos.X, pos.Y, z, false, false, false, true);
                timer = GetGameTimer();

                while (!HasCollisionLoadedAroundEntity(Game.PlayerPed.Handle))
                {
                    if (GetGameTimer() - timer > 1000)
                        break;
                    await BaseScript.Delay(0);
                }

                var found = GetGroundZFor_3dCoord(pos.X, pos.Y, z, ref ground, false);

                if (found)
                {
                    SetEntityCoords(Game.PlayerPed.Handle, pos.X, pos.Y, ground, false, false, false, true);
                    break;
                }

                await BaseScript.Delay(0);
            }
        }
    }
}
