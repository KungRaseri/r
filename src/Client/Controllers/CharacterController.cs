using CitizenFX.Core;
using OpenRP.Framework.Client.Classes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class CharacterController : ClientAccessor
    {
        internal CharacterController(ClientMain client) : base(client)
        {
            Client.GetExport("spawnmanager").setAutoSpawn(false);
            //Client.GetExport("spawnmanager").spawnPlayer(SpawnPosition());
            //Client.GetExport("spawnmanager").forceRespawn();
            Client.Events["playerSpawned"] += new Action(OnPlayerSpawned);

            FirstSpawn();
        }

        private static void FirstSpawn()
        {
            DisplayRadar(false);
            SetTimecycleModifier("hud_def_blur");

            var cam = new Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true))
            {
                Position = new Vector3(-1472.59f, -1445.63f, 44.82f),
                Rotation = Vector3.Zero,
                FieldOfView = 75f
            };

            cam.IsActive = true;
            RenderScriptCams(true, false, 0, true, true);
        }

        private async void OnPlayerSpawned()
        {
            await Position.Teleport(Vector3.Zero);
        }

        private dynamic SpawnPosition()
        {
            dynamic obj = new ExpandoObject();
            obj.x = 0;
            obj.y = 0;
            obj.z = 0;
            obj.heading = 0;
            return obj;
        }
    }
}
