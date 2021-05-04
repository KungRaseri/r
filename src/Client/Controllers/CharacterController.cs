using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Common.Enumeration;
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
            //Client.GetExport("spawnmanager").setAutoSpawn(false);
            //Client.GetExport("spawnmanager").spawnPlayer(SpawnPosition());
            //Client.GetExport("spawnmanager").forceRespawn();
            Client.Events["playerSpawned"] += new Action(OnPlayerSpawned);
            Client.Event.RegisterNuiEvent(NuiEvent.SAVE_NEW_CHARACTER, new Action<dynamic>(OnSaveNewCharacter));

            FirstSpawn();
        }

        private void OnSaveNewCharacter(dynamic args)
        {
            Client.Event.TriggerServerEvent(ServerEvent.SAVE_NEW_CHARACTER, args);
        }

        private async static void FirstSpawn()
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
            await BaseScript.Delay(2000);
            UIElement.ToggleNuiModule("TOGGLE_CHARACTER_SELECT", true, true, true);
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
