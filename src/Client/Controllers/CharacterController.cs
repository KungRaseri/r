using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Client.Classes.StyleComponents;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static OpenRP.Framework.Client.Classes.StyleComponents.PedStyle;

namespace OpenRP.Framework.Client.Controllers
{
    public class CharacterController : ClientAccessor
    {
        static List<string> _peds;
        private const float _heading = 160f;
        private Vector3 _pos = new Vector3(683.852f, 570.629f, 130.461f);
        Camera _cam;

        internal CharacterController(ClientMain client) : base(client)
        {
            _peds = new List<string>();
            PedListBuilder();

            Client.Events["playerSpawned"] += new Action(OnPlayerSpawned);
            Client.Event.RegisterNuiEvent(NuiEvent.SAVE_NEW_CHARACTER, new Action<dynamic>(OnSaveNewCharacter));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_CHARACTER_MODEL, new Action<dynamic>(OnSetCharacterModel));
            Client.Event.RegisterNuiEvent(NuiEvent.AGGREGATE_DATA, new Action<dynamic>(OnAggregateData));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_PED_COMPONENT, new Action<dynamic>(OnSetPedComponent));

            FirstSpawn();
        }

        internal static void PedListBuilder()
        {
            foreach (PedHash value in Enum.GetValues(typeof(PedHash)))
            {
                var ped = value.ToString();
                if (ped != "FreemodeFemale01" && ped != "FreemodeMale01")
                    _peds.Add(ped);
            }

            _peds.Sort();

            _peds.Insert(0, "FreemodeFemale01");
            _peds.Insert(1, "FreemodeMale01");
        }

        private void OnSaveNewCharacter(dynamic args)
        {
            Client.Event.TriggerServerEvent(ServerEvent.SAVE_NEW_CHARACTER, args);
            Client.GetExport("spawnmanager").spawnPlayer(SpawnPosition());
        }

        private async void FirstSpawn()
        {
            DisplayRadar(false);
            SetTimecycleModifier("hud_def_blur");

            _cam = new Camera(CreateCam("DEFAULT_SCRIPTED_CAMERA", true))
            {
                Position = new Vector3(-1472.59f, -1445.63f, 44.82f),
                Rotation = Vector3.Zero,
                FieldOfView = 75f
            };

            _cam.IsActive = true;
            RenderScriptCams(true, false, 0, true, true);
            await BaseScript.Delay(1000);
            UIElement.ToggleNuiModule("TOGGLE_CHARACTER_SELECT", true, true, true);
        }

        private async void OnPlayerSpawned()
        {
            await WorldHelper.FadeOut(1000);
            Client.Event.TriggerServerEvent(ServerEvent.SET_PLAYER_ROUTING_BUCKET);
            NetworkOverrideClockTime(12, 0, 0);
            PauseClock(true);
            await Position.Teleport(_pos, true);
            Game.PlayerPed.Heading = _heading;
            _cam.Position = WorldHelper.PosOffset(_pos, _heading, 2);
            _cam.PointAt(Game.PlayerPed);
            SetTimecycleModifier("default");
            SetOverrideWeather("EXTRASUNNY");
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName = "LIST_OF_PEDS", _peds }));
            UIElement.ToggleNuiModule("TOGGLE_PED_SELECT", true, true, true);
            await WorldHelper.FadeIn(1000);
        }

        private dynamic SpawnPosition()
        {
            dynamic obj = new ExpandoObject();
            obj.x = 0;
            obj.y = 0;
            obj.z = 0;
            obj.heading = 0;
            obj.skipFade = true;
            return obj;
        }
    }
}
