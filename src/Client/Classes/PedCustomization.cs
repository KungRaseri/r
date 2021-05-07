using CitizenFX.Core;
using OpenRP.Framework.Client.Classes.StyleComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRP.Framework.Client.Classes
{
    internal class PedCustomization
    {
        Model _model => Game.PlayerPed.Model;
        HeadBlend _head;
        PedHair _hair;
        int _eye;
        List<PedComponent> _comps;
        List<PedProp> _props;
        List<FacialSlider> _facialSliders;
        List<PedOverlay> _overlays;
    }
}
