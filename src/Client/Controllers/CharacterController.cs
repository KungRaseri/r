using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Client.Classes.StyleComponents;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static OpenRP.Framework.Client.Classes.StyleComponents.PedStyle;

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

        internal CharacterController(ClientMain client) : base(client)
        {
            _peds = new List<string>();
            _heading = 160f;
            _pos = new Vector3(683.852f, 570.629f, 130.461f);
            _steps = new float[] { 0.7f, 0.25f, 0f, -0.3f, -0.75f };
            _step = 0;
            _freeze = false;
            StyleListBuilders();

            Client.Events["playerSpawned"] += new Action(OnPlayerSpawned);
            Client.Event.RegisterNuiEvent(NuiEvent.SAVE_NEW_CHARACTER, new Action<dynamic>(OnSaveNewCharacter));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_CHARACTER_MODEL, new Action<dynamic>(OnSetCharacterModel));
            Client.Event.RegisterNuiEvent(NuiEvent.AGGREGATE_DATA, new Action<dynamic>(OnAggregateData));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_PED_COMPONENT, new Action<dynamic>(OnSetPedComponent));
            Client.Event.RegisterNuiEvent(NuiEvent.SET_COMPONENT_COLOR, new Action<dynamic>(OnSetPedComponentColor));

            Client.Event.RegisterEvent(ClientEvent.GET_CHARACTER_OBJECT_ID, new Action<dynamic>(OnGetCharacterObjectId));

            FirstSpawn();
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

            foreach (PedOverlays value in Enum.GetValues(typeof(PedOverlays)))
            {
                var name = value.ToString();
                var temp = new PedOverlay(name);

                if (name == "Eyebrows")
                    temp.Value = 1;

                Game.PlayerPed.State.Set(name, temp, false);
            }


            foreach (FacialSliders value in Enum.GetValues(typeof(FacialSliders)))
            {
                var name = value.ToString();
                var temp = new FacialSlider(name);

                Game.PlayerPed.State.Set(name, temp, false);
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
            await BaseScript.Delay(50);
            NetworkOverrideClockTime(12, 0, 0);
            PauseClock(true);
            await Position.Teleport(_pos, true);
            Game.PlayerPed.Heading = _heading;
            _cam.Position = WorldHelper.PosOffset(_pos, _heading, 2);
            _cam.PointAt(Game.PlayerPed);
            SetTimecycleModifier("default");
            SetOverrideWeather("EXTRASUNNY");
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName = "LIST_OF_PEDS", _peds }));
            UIElement.ToggleNuiModule("TOGGLE_PED_SELECT", true, true, true, true);
            Game.PlayerPed.IsPositionFrozen = true;
            Client.RegisterTickHandler(WorldHelper.DisableAllControls);
            Client.RegisterTickHandler(CameraControls);
            await WorldHelper.FadeIn(1000);
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
                _cam.Position = WorldHelper.PosOffset(_pos, _heading, 2);
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
                    await WorldHelper.RequestAnimationDictionary(animDict);
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
            _cam.Position = WorldHelper.PosOffset(temp, _heading, 0.6f);
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
