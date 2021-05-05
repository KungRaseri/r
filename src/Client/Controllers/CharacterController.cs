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
        int _face1;
        int _face2;
        int _skin1;
        int _skin2;
        float _faceBlend;
        float _skinBlend;

        private const float _heading = 160f;
        private Vector3 _pos = new Vector3(683.852f, 570.629f, 130.461f);
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
            Client.Event.RegisterNuiEvent(NuiEvent.SET_PED_COMPONENT, new Action<dynamic>(OnSetPedComponent));

            FirstSpawn();
        }

        private void OnSetPedComponent(dynamic args)
        {
            var item = int.Parse(args.itemIndex);
            var texture = int.Parse(args.textureIndex);

            if (args.name == "FaceBlend" || args.name == "SkinBlend")
            {
                var slider = (float)args.sliderValue;

                if (args.name == "FaceBlend")
                {
                    _face1 = item;
                    _face2 = texture;
                    _faceBlend = slider;
                }
                else
                {
                    _skin1 = item;
                    _skin2 = texture;
                    _skinBlend = slider;
                }

                Debug.WriteLine(slider.ToString());
                SetPedHeadBlendData(Game.PlayerPed.Handle, _face1, _face2, 0, _skin1, _skin2, 0, _faceBlend, _skinBlend, 0, false);
            }
            else
            {
                Enum.TryParse(args.name, out PedComponents comp);
                Game.PlayerPed.Style[comp].SetVariation(item, texture);
                SendComponentData();
            }
        }

        private void OnAggregateData(dynamic args)
        {
            Aggregate();
            SendComponentData();
        }

        private static void Aggregate()
        {
            var comps = new List<string>();
            foreach (PedComponents value in Enum.GetValues(typeof(PedComponents)))
            {
                comps.Add(value.ToString());
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

        private async void OnSetCharacterModel(dynamic args)
        {
            string stringPed = args.ped;
            await SetModel(stringPed);
        }

        private async Task SetModel(string stringPed)
        {
            Enum.TryParse(stringPed, out PedHash ped);
            var model = new Model(ped);
            await Game.Player.ChangeModel(model);
            Game.PlayerPed.Style.SetDefaultClothes();
            float ground = 0f;
            GetGroundZFor_3dCoord(_pos.X, _pos.Y, _pos.Z, ref ground, false);
            Game.PlayerPed.Position = new Vector3(_pos.X, _pos.Y, ground);
            Game.PlayerPed.Heading = _heading;

            if (IsFreemode(ped))
                SetPedHeadBlendData(Game.PlayerPed.Handle, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0f, false);
        }

        private static bool IsFreemode(PedHash ped)
        {
            if (ped == PedHash.FreemodeFemale01 || ped == PedHash.FreemodeMale01)
                return true;

            return false;
        }

        private static void SendComponentData()
        {
            Dictionary<string, PedComponent> components = new Dictionary<string, PedComponent>();

            foreach (var item in Enum.GetNames(typeof(PedComponents)))
            {
                Enum.TryParse(item, out PedComponents comp);
                components.Add(item, Game.PlayerPed.Style[comp]);
            }

            var eventName = "PED_COMPONENT_DATA";
            var hash = (PedHash)Game.PlayerPed.Model.Hash;
            foreach (var item in components)
            {
                Enum.TryParse(item.Key, out PedComponents comp);
                if (!IsFreemode(hash) || (IsFreemode(hash) && comp != PedComponents.Face))
                {
                    var data = new
                    {
                        eventName,
                        name = item.Key,
                        comp = item.Value
                    };

                    SendNuiMessage(JsonConvert.SerializeObject(data));
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
