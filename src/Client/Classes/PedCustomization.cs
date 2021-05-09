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
        public string Model { get; set; }
        public HeadBlend Head { get; set; }
        public PedHair Hair { get; set; }
        public int Eye { get; set; }
        public Dictionary<string, AltPedComponent> PedComponents { get; set; }
        public Dictionary<string, AltPedComponent> PedProps { get; set; }
        public Dictionary<string, FacialSlider> FacialSliders { get; set; }
        public Dictionary<string, PedOverlay> Overlays { get; set; }
    }
}
