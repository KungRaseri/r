using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        internal static void OnSetPedComponent(dynamic args)
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

                SetPedHeadBlendData(Game.PlayerPed.Handle, _face1, _face2, 0, _skin1, _skin2, 0, _faceBlend, _skinBlend, 0, false);
            }
            else
            {
                Enum.TryParse(args.name, out PedComponents comp);
                Game.PlayerPed.Style[comp].SetVariation(item, texture);
                SendComponentData();
            }
        }

        internal static void OnAggregateData(dynamic args)
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
    }
}
