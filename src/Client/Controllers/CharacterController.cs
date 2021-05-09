using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Client.Classes.StyleComponents;
using OpenRP.Framework.Common.Classes;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static OpenRP.Framework.Client.Classes.StyleComponents.PedStyle;
using static OpenRP.Framework.Client.Classes.WorldHelper;

namespace OpenRP.Framework.Client.Controllers
{
    public class CharacterController : ClientAccessor
    {
        static string _id;
        static List<string> _peds;
        private float _heading;
        private Vector3 _pos;
        Camera _cam;
        bool _closeup;
        float[] _steps;
        int _step;
        bool _freeze;
        List<dynamic> _characters;

        internal CharacterController(ClientMain client) : base(client)
        {
            _peds = new List<string>();
            _heading = 160f;
            _pos = new Vector3(683.852f, 570.629f, 130.461f);
            _steps = new float[] { 0.7f, 0.25f, 0f, -0.3f, -0.75f };
            _step = 0;
            _freeze = false;
            _characters = new List<dynamic>();
            StyleListBuilders();

            Client.Event.RegisterNuiEvent(NuiEvent.SAVE_NEW_CHARACTER, new Action<dynamic>(OnSaveNewCharacter));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_CHARACTER_MODEL, new Action<dynamic>(OnSetCharacterModel));
            Client.Event.RegisterNuiEvent(NuiEvent.AGGREGATE_DATA, new Action<dynamic>(OnAggregateData));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_PED_COMPONENT, new Action<dynamic>(OnSetPedComponent));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_COMPONENT_COLOR, new Action<dynamic>(OnSetPedComponentColor));
            Client.Event.RegisterNuiEvent(NuiEvent.SAVE_CHARACTER_CUSTOMIZATION, new Action<dynamic>(OnSaveCharacterCustomization));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_NUI_FOCUS, new Action<dynamic>(OnSetNuiFocus));
            Client.Event.RegisterNuiEvent(NuiEvent.POPULATE_CHARACTER_SELECT, new Action<dynamic>(OnPopulateCharacterSelect));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_CHARACTER, new Action<dynamic>(OnSetCharacter));

            Client.Event.RegisterEvent(ClientEvent.GET_CHARACTER_OBJECT_ID, new Action<dynamic>(OnGetCharacterObjectId));
            Client.Event.RegisterEvent(ClientEvent.RETRIEVE_CHARACTERS, new Action<dynamic>(OnRetrieveCharacters));
            Client.GetExport("spawnmanager").spawnPlayer(SpawnPosition());

            FirstSpawn();
        }

        private async void OnSetCharacter(dynamic args)
        {
            await FadeOut(1000);
            SetTimecycleModifier("default");
            var temp = JsonConvert.DeserializeObject<List<CharacterData>>(JsonConvert.SerializeObject(_characters));
            var character = temp.Find(e => e.Id == args.id);
            PedCustomization customization = JsonConvert.DeserializeObject<PedCustomization>(character.Customization);
            SetCustomization(customization);
            await Position.Teleport(_pos, true);
            Game.PlayerPed.Heading = _heading;
            _cam.Position = PosOffset(_pos, _heading, 2);
            _cam.PointAt(Game.PlayerPed);
            await BaseScript.Delay(500);
            await FadeIn(1000);
        }

        private void OnRetrieveCharacters(dynamic args)
        {
            _characters = JsonConvert.DeserializeObject<List<dynamic>>(args);
            var data = new { eventName = "AGGREGATE_CHARACTERS", _characters };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }

        private void OnPopulateCharacterSelect(dynamic args)
        {
            Client.Event.TriggerServerEvent(ServerEvent.RETRIEVE_CHARACTERS);
        }

        private void OnSetNuiFocus(dynamic args)
        {
            SetNuiFocusKeepInput(args.value);
        }

        private async void OnSaveCharacterCustomization(dynamic args)
        {
            SaveToDb();
            await FadeOut(1000);
            Client.UnregisterTickHandler(DisableAllControls);
            Client.UnregisterTickHandler(CameraControls);
            ClearOverrideWeather();
            Game.PauseClock(false);
            NetworkClearClockTimeOverride();
            RenderScriptCams(false, false, 0, false, false);
            Game.PlayerPed.IsPositionFrozen = false;
            Client.Event.TriggerServerEvent(ServerEvent.SET_PLAYER_ROUTING_BUCKET, false);
            SetNuiFocus(false, false);
            await BaseScript.Delay(5000);
            DoScreenFadeIn(1000);
        }

        private void SaveToDb()
        {
            var compDict = new Dictionary<string, AltPedComponent>();

            foreach (var name in Enum.GetNames(typeof(PedComponents)))
            {
                Enum.TryParse(name, out PedComponents value);
                PedComponent temp = Game.PlayerPed.Style[value];
                AltPedComponent comp = new AltPedComponent()
                {
                    Index = temp.Index,
                    Texture = temp.TextureIndex
                };
                compDict.Add(name, comp);
            }

            var propDict = new Dictionary<string, AltPedComponent>();

            foreach (var name in Enum.GetNames(typeof(PedProps)))
            {
                Enum.TryParse(name, out PedProps value);
                PedProp temp = Game.PlayerPed.Style[value];
                AltPedComponent comp = new AltPedComponent()
                {
                    Index = temp.Index,
                    Texture = temp.TextureIndex
                };
                propDict.Add(name, comp);
            }

            var faceDict = new Dictionary<string, FacialSlider>();

            foreach (var name in Enum.GetNames(typeof(FacialSliders)))
            {
                FacialSlider comp = GetState<FacialSlider>(Game.PlayerPed, name);
                faceDict.Add(name, comp);
            }

            var overlayDict = new Dictionary<string, PedOverlay>();

            foreach (var name in Enum.GetNames(typeof(PedOverlays)))
            {
                PedOverlay comp = GetState<PedOverlay>(Game.PlayerPed, name);
                overlayDict.Add(name, comp);
            }

            var data = new PedCustomization()
            {
                Model = Enum.GetName(typeof(PedHash), (uint)Game.PlayerPed.Model.Hash),
                Head = Game.PlayerPed.State.Get("HeadBlend"),
                Hair = GetState<PedHair>(Game.PlayerPed, "PedHair"),
                Eye = GetPedEyeColor(Game.PlayerPed.Handle),
                PedComponents = compDict,
                PedProps = propDict,
                FacialSliders = faceDict,
                Overlays = overlayDict
            };

            Client.Event.TriggerServerEvent(ServerEvent.STORE_CHARACTER_CUSTOMIZATION, _id, JsonConvert.SerializeObject(data));
        }

        private void OnGetCharacterObjectId(dynamic args)
        {
            _id = args;
        }

        private static void StyleListBuilders()
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
            OnPlayerSpawned();
        }

        private async void FirstSpawn()
        {
            Client.Event.TriggerServerEvent(ServerEvent.SET_PLAYER_ROUTING_BUCKET, true);
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
            await FadeOut(1000);
            await BaseScript.Delay(50);
            NetworkOverrideClockTime(12, 0, 0);
            Game.PauseClock(true);
            await Position.Teleport(_pos, true);
            Game.PlayerPed.Heading = _heading;
            _cam.Position = PosOffset(_pos, _heading, 2);
            _cam.PointAt(Game.PlayerPed);
            SetTimecycleModifier("default");
            SetOverrideWeather("EXTRASUNNY");
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName = "LIST_OF_PEDS", _peds }));
            UIElement.ToggleNuiModule("TOGGLE_PED_SELECT", true, true, true, true);
            Game.PlayerPed.IsPositionFrozen = true;
            Client.RegisterTickHandler(DisableAllControls);
            Client.RegisterTickHandler(CameraControls);
            await FadeIn(1000);
        }

        internal async Task CameraControls()
        {
            if (Game.IsDisabledControlPressed((int)InputMode.MouseAndKeyboard, Control.MoveLeftOnly))
            {
                Game.PlayerPed.Heading -= 15;
                await BaseScript.Delay(50);
            }
            if (Game.IsDisabledControlPressed((int)InputMode.MouseAndKeyboard, Control.MoveRightOnly))
            {
                Game.PlayerPed.Heading += 15;
                await BaseScript.Delay(50);
            }
            if (Game.IsDisabledControlJustPressed((int)InputMode.MouseAndKeyboard, Control.Pickup) && !_closeup)
            {
                OffsetCameraVertical();
                _closeup = true;

            }
            if (Game.IsDisabledControlJustPressed((int)InputMode.MouseAndKeyboard, Control.Cover))
            {
                _cam.Position = PosOffset(_pos, _heading, 2);
                _cam.PointAt(Game.PlayerPed);
                _closeup = false;
            }
            if (Game.IsDisabledControlJustPressed((int)InputMode.MouseAndKeyboard, Control.MoveUpOnly) && _closeup)
            {
                var temp = _step - 1;
                if (temp >= 0)
                {
                    _step -= 1;
                    OffsetCameraVertical();
                }
            }
            if (Game.IsDisabledControlJustPressed((int)InputMode.MouseAndKeyboard, Control.MoveDownOnly) && _closeup)
            {
                var temp = _step + 1;
                if (temp <= _steps.Length - 1)
                {
                    _step += 1;
                    OffsetCameraVertical();
                }
            }
            if (Game.IsDisabledControlJustPressed((int)InputMode.MouseAndKeyboard, Control.Duck))
            {
                if (!_freeze)
                {
                    var animDict = "mp_sleep";
                    await RequestAnimationDictionary(animDict);
                    Game.PlayerPed.Task.PlayAnimation(animDict, "bind_pose_180", 1000f, -1, AnimationFlags.StayInEndFrame);
                    _freeze = true;
                }
                else
                {
                    Game.PlayerPed.Task.ClearAllImmediately();
                    _freeze = false;
                }
            }
        }

        private void OffsetCameraVertical()
        {
            var temp = _pos;
            temp.Z += _steps[_step];
            _cam.Position = PosOffset(temp, _heading, 0.6f);
            _cam.PointAt(new Vector3(_pos.X, _pos.Y, temp.Z));
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
