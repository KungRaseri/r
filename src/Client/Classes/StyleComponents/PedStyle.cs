using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    public static class PedStyle
    {
        static int _face1;
        static int _face2;
        static int _skin1;
        static int _skin2;
        static float _faceBlend;
        static float _skinBlend;
        static int _hair1 = 0;
        static int _hair2 = 0;

        internal static void OnSetPedComponent(dynamic args)
        {
            int item = int.Parse(args.itemIndex);
            int texture = int.Parse(args.textureIndex);
            var slider = (float)args.sliderValue;

            if (args.type == "blends")
            {

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

                SetPedHeadBlendData(Game.PlayerPed.Handle, _face1, _face2, 0, _skin1, _skin2, 0, _faceBlend, _skinBlend, 0, false);
            }
            else if (args.type == "comps")
            {
                Enum.TryParse(args.name, out PedComponents comp);
                Game.PlayerPed.Style[comp].SetVariation(item, texture);
                SendComponentData<PedComponents, PedComponent>(false);
            }
            else if (args.type == "props")
            {
                Enum.TryParse(args.name, out PedProps comp);
                Game.PlayerPed.Style[comp].SetVariation(item, texture);
                SendComponentData<PedProps, PedProp>(false);
            }
            else if (args.type == "overlays")
            {
                PedOverlay overlay = JsonConvert.DeserializeObject<PedOverlay>(JsonConvert.SerializeObject(Game.PlayerPed.State.Get(args.name)));
                overlay.Opacity = slider;
                overlay.SetVariation(item);
                Game.PlayerPed.State.Set(args.name, overlay, false);
                SendComponentData<PedOverlays, PedOverlay>(false);
            }
        }

        internal async static void OnAggregateData(dynamic args)
        {
            await SetModel(args.ped);
            Aggregate<PedComponents, PedComponent>();
            Aggregate<PedProps, PedProp>();
            Aggregate<PedOverlays, PedOverlay>();
        }

        internal static void OnSetPedComponentColor(dynamic args)
        {
            if (args.type == "comps")
            {
                if (args.target == "primary")
                {
                    SetPedHairColor(Game.PlayerPed.Handle, args.index, _hair2);
                    _hair1 = args.index;
                }
                else
                {
                    SetPedHairColor(Game.PlayerPed.Handle, _hair1, args.index);
                    _hair2 = args.index;
                }
            }
            else if (args.type == "overlays")
            {
                PedOverlay overlay = JsonConvert.DeserializeObject<PedOverlay>(JsonConvert.SerializeObject(Game.PlayerPed.State.Get(args.name)));

                if (args.target == "primary")
                    overlay.SetPrimaryColor(args.index);
                else
                    overlay.SetSecondaryColor(args.index);

                Game.PlayerPed.State.Set(args.name, overlay, false);
            }
            else if (args.type == "eyes")
                SetPedEyeColor(Game.PlayerPed.Handle, args.index);
        }

        private static void Aggregate<PedEnum, PedVariation>()
        {
            var comps = new List<string>();
            var type = "";

            if (typeof(PedEnum) == typeof(PedComponents))
                type = "comps";
            else if (typeof(PedEnum) == typeof(PedProps))
                type = "props";
            else if (typeof(PedEnum) == typeof(PedOverlays))
                type = "overlays";

            foreach (PedEnum value in Enum.GetValues(typeof(PedEnum)))
            {
                comps.Add(value.ToString());
            }
            var data = new
            {
                eventName = "AGGREGATE_COMPONENTS",
                comps = comps.ToArray(),
                type
            };

            SendNuiMessage(JsonConvert.SerializeObject(data));
            SendComponentData<PedEnum, PedVariation>(true);
        }

        internal static async void OnSetCharacterModel(dynamic args)
        {
            string stringPed = args.ped;
            await SetModel(stringPed);
        }

        public static async Task SetModel(string stringPed)
        {
            var pos = Game.PlayerPed.Position;
            var heading = Game.PlayerPed.Heading;
            Enum.TryParse(stringPed, out PedHash ped);
            var model = new Model(ped);
            await Game.Player.ChangeModel(model);
            Game.PlayerPed.Style.SetDefaultClothes();
            float ground = 0f;
            GetGroundZFor_3dCoord(pos.X, pos.Y, pos.Z, ref ground, false);
            Game.PlayerPed.Position = new Vector3(pos.X, pos.Y, ground);
            Game.PlayerPed.Heading = heading;

            if (IsFreemode(ped))
                SetPedHeadBlendData(Game.PlayerPed.Handle, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0f, false);
        }

        private static bool IsFreemode(PedHash ped)
        {
            if (ped == PedHash.FreemodeFemale01 || ped == PedHash.FreemodeMale01)
                return true;

            return false;
        }

        private static void SendComponentData<PedEnum, PedVariation>(bool send)
        {
            Dictionary<string, IPedVariation> components = new Dictionary<string, IPedVariation>();

            foreach (var item in Enum.GetNames(typeof(PedEnum)))
            {
                if (typeof(PedVariation) == typeof(PedComponent))
                {
                    Enum.TryParse(item, out PedComponents comp);
                    components.Add(item, Game.PlayerPed.Style[comp]);
                }
                else if (typeof(PedVariation) == typeof(PedProp))
                {
                    Enum.TryParse(item, out PedProps comp);
                    components.Add(item, Game.PlayerPed.Style[comp]);
                }
                else if (typeof(PedVariation) == typeof(PedOverlay))
                {
                    PedOverlay comp = JsonConvert.DeserializeObject<PedOverlay>(JsonConvert.SerializeObject(Game.PlayerPed.State.Get(item)));
                    components.Add(item, comp);
                }
            }

            Debug.WriteLine(JsonConvert.SerializeObject(components));

            string eventName;

            if (send)
                eventName = "PED_COMPONENT_DATA";
            else
                eventName = "PED_COMPONENT_UPDATE";

            var hash = (PedHash)Game.PlayerPed.Model.Hash;
            foreach (var item in components)
            {
                string type = "";

                if (typeof(PedVariation) == typeof(PedComponent))
                {
                    Enum.TryParse(item.Key, out PedComponents comp);

                    if (!IsFreemode(hash) || (IsFreemode(hash) && comp != PedComponents.Face))
                        type = "comps";
                }
                else if (typeof(PedVariation) == typeof(PedProp))
                    type = "props";
                else if (typeof(PedVariation) == typeof(PedOverlay))
                    type = "overlays";

                SendComponent(eventName, item, type);
            }
        }

        private static void SendComponent(string eventName, KeyValuePair<string, IPedVariation> item, string type)
        {
            var data = new
            {
                eventName,
                name = item.Key,
                comp = item.Value,
                type
            };

            SendNuiMessage(JsonConvert.SerializeObject(data));
        }
    }
}
