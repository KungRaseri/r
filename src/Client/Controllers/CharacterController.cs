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
        private const float _heading = 160f;
        Camera _cam;
        List<string> _peds;

        internal CharacterController(ClientMain client) : base(client)
        {
            _peds = new List<string>();
            PedListBuilder();

            Client.Events["playerSpawned"] += new Action(OnPlayerSpawned);
            Client.Event.RegisterNuiEvent(NuiEvent.SAVE_NEW_CHARACTER, new Action<dynamic>(OnSaveNewCharacter));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_CHARACTER_MODEL, new Action<dynamic>(OnSetCharacterModel));
            Client.Event.RegisterNuiEvent(NuiEvent.AGGREGATE_DATA, new Action<dynamic>(OnAggregateData));

            FirstSpawn();
        }

        private void OnAggregateData(dynamic args)
        {
            var components = Game.PlayerPed.Style.GetAllComponents();
            var props = Game.PlayerPed.Style.GetAllProps();
            var variations = Game.PlayerPed.Style.GetAllVariations();

            Aggregate();
            SendComponentData(components);
        }

        private static void Aggregate()
        {
            var comps = new List<string>();
            foreach (var value in Enum.GetValues(typeof(PedComponents)))
            {
                comps.Add(value.ToString());
                Debug.WriteLine(value.ToString());
            }
            var data = new
            {
                eventName = "AGGREGATE_COMPONENTS",
                comps = comps.ToArray()
            };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }

        private void PedListBuilder()
        {
            foreach (var value in Enum.GetValues(typeof(PedHash)))
                _peds.Add(value.ToString());

            _peds.Sort();
        }

        private async void OnSetCharacterModel(dynamic args)
        {
            string stringPed = args.ped;
            await SetModel(stringPed);
        }

        private static async Task SetModel(string stringPed)
        {
            PedHash ped;
            Enum.TryParse(stringPed, out ped);
            var model = new Model(ped);
            await Game.Player.ChangeModel(model);
            Game.PlayerPed.Style.SetDefaultClothes();
            Game.PlayerPed.Heading = _heading;

            if (stringPed == PedHash.FreemodeFemale01.ToString() || stringPed == PedHash.FreemodeMale01.ToString())
                SetPedHeadBlendData(Game.PlayerPed.Handle, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0f, false);
        }

        private static void SendComponentData(PedComponent[] components)
        {
            var eventName = "PED_COMPONENT_DATA";
            foreach (var item in Enum.GetValues(typeof(PedComponents)))
            {
                try
                {
                    PedComponents value;
                    Enum.TryParse(item.ToString(), out value);
                    var comp = components[(int)value];

                    var pedComp = new
                    {
                        item = item.ToString(),
                        value,
                        comp
                    };

                    var data = new
                    {
                        eventName,
                        pedComp
                    };

                    SendNuiMessage(JsonConvert.SerializeObject(data));
                }
                catch
                {
                    continue;
                }
            }
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
            var pos = new Vector3(683.852f, 570.629f, 130.461f);
            await Position.Teleport(pos, true);
            Game.PlayerPed.Heading = _heading;
            _cam.Position = WorldHelper.PosOffset(pos, _heading, 2);
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
