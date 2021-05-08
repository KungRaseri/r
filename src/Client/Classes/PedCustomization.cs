using CitizenFX.Core;
using OpenRP.Framework.Client.Classes.StyleComponents;
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
        Model _model => Game.PlayerPed.Model;
        public dynamic Head { get; set; }
        public PedHair Hair { get; set; }
        int _eye => GetPedEyeColor(Game.PlayerPed.Handle);
        public Dictionary<string, PedComponent> PedComponents { get; set; }
        public Dictionary<string, PedProp> PedProps { get; set; }
        public Dictionary<string, FacialSlider> FacialSliders { get; set; }
        public Dictionary<string, PedOverlay> Overlays { get; set; }
    }
}
