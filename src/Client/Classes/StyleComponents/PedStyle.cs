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
        internal static void OnSetPedComponent(dynamic args)
        {
            int item = int.Parse(args.itemIndex);
            int texture = int.Parse(args.textureIndex);
            var slider = (float)args.sliderValue;

            if (args.type == "blends")
            {
                dynamic comp = Game.PlayerPed.State.Get("HeadBlend");
                if (args.name == "FaceBlend")
                {
                    comp.Face1 = item;
                    comp.Face2 = texture;
                    comp.FaceBlend = slider;
                }
                else
                {
                    comp.Skin1 = item;
                    comp.Skin2 = texture;
                    comp.SkinBlend = slider;
                }

                SetPedHeadBlendData(Game.PlayerPed.Handle, comp.Face1, comp.Face2, 0, comp.Skin1, comp.Skin2, 0, comp.FaceBlend, comp.SkinBlend, 0, false);
                Game.PlayerPed.State.Set("HeadBlend", comp, false);
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
                PedOverlay comp = WorldHelper.GetState<PedOverlay>(Game.PlayerPed, args.name);
                comp.Value = slider;
                comp.SetVariation(item);
                Game.PlayerPed.State.Set(args.name, comp, false);
                SendComponentData<PedOverlays, PedOverlay>(false);
            }
            else if (args.type == "facials")
            {
                FacialSlider comp = WorldHelper.GetState<FacialSlider>(Game.PlayerPed, args.name);
                comp.Value = slider;
                comp.SetFeature(slider);
                Game.PlayerPed.State.Set(args.name, comp, false);
                SendComponentData<FacialSliders, FacialSlider>(false);
            }
        }

        internal async static void OnAggregateData(dynamic args)
        {
            await SetModel(args.ped);
            Aggregate<PedComponents, PedComponent>();
            Aggregate<PedProps, PedProp>();
            Aggregate<PedOverlays, PedOverlay>();
            Aggregate<FacialSliders, FacialSlider>();
        }

        internal static void OnSetPedComponentColor(dynamic args)
        {
            if (args.type == "comps")
            {
                PedHair comp = WorldHelper.GetState<PedHair>(Game.PlayerPed, "PedHair");
                comp.SetHair(args.index, args.target);
                Game.PlayerPed.State.Set("PedHair", comp, false);
            }
            else if (args.type == "overlays")
            {
                PedOverlay comp = WorldHelper.GetState<PedOverlay>(Game.PlayerPed, args.name);
                comp.SetColor(args.index, args.target);
                Game.PlayerPed.State.Set(args.name, comp, false);
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
            else if (typeof(PedEnum) == typeof(FacialSliders))
                type = "facials";

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
            {
                var blend = new HeadBlend();
                SetPedHeadBlendData(Game.PlayerPed.Handle, blend.Face1, blend.Face2, 0, blend.Skin1, blend.Skin2, 0, blend.FaceBlend, blend.SkinBlend, 0, false);
                Game.PlayerPed.State.Set("HeadBlend", blend, false);

                var hair = new PedHair();
                Game.PlayerPed.State.Set("PedHair", hair, false);

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
                    PedOverlay comp = WorldHelper.GetState<PedOverlay>(Game.PlayerPed, item);
                    components.Add(item, comp);
                }
                else if (typeof(PedVariation) == typeof(FacialSlider))
                {
                    FacialSlider comp = WorldHelper.GetState<FacialSlider>(Game.PlayerPed, item);
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
                else if (typeof(PedVariation) == typeof(FacialSlider))
                    type = "facials";

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
