using CitizenFX.Core;
using OpenRP.Framework.Client.Classes.StyleComponents;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    internal class PedCustomization
    {
        public string Model
        {
            get
            {
                return Enum.GetName(typeof(PedHash), (uint)Game.PlayerPed.Model.Hash);
            }
        }

        public dynamic Head
        {
            get
            {
                return Game.PlayerPed.State.Get("HeadBlend");
            }
        }

        public PedHair Hair
        {
            get
            {
                return WorldHelper.GetState<PedHair>(Game.PlayerPed, "PedHair");
            }
        }

        public int Eye
        {
            get
            {
                return GetPedEyeColor(Game.PlayerPed.Handle);
            }
        }

        public Dictionary<string, PedComponent> PedComponents
        {
            get
            {
                var compDict = new Dictionary<string, PedComponent>();

                foreach (var name in Enum.GetNames(typeof(PedComponents)))
                {
                    Enum.TryParse(name, out PedComponents value);
                    PedComponent comp = Game.PlayerPed.Style[value];
                    compDict.Add(name, comp);
                }

                return compDict;
            }
        }
        public Dictionary<string, PedProp> PedProps
        {
            get
            {
                var propDict = new Dictionary<string, PedProp>();

                foreach (var name in Enum.GetNames(typeof(PedProps)))
                {
                    Enum.TryParse(name, out PedProps value);
                    PedProp comp = Game.PlayerPed.Style[value];
                    propDict.Add(name, comp);
                }

                return propDict;
            }
        }
        public Dictionary<string, FacialSlider> FacialSliders
        {
            get
            {
                var faceDict = new Dictionary<string, FacialSlider>();

                foreach (var name in Enum.GetNames(typeof(FacialSliders)))
                {
                    FacialSlider comp = WorldHelper.GetState<FacialSlider>(Game.PlayerPed, name);
                    faceDict.Add(name, comp);
                }

                return faceDict;
            }
        }

        public Dictionary<string, PedOverlay> Overlays
        {
            get
            {
                var overlayDict = new Dictionary<string, PedOverlay>();

                foreach (var name in Enum.GetNames(typeof(PedOverlays)))
                {
                    PedOverlay comp = WorldHelper.GetState<PedOverlay>(Game.PlayerPed, name);
                    overlayDict.Add(name, comp);
                }

                return overlayDict;
            }
        }
    }
}
