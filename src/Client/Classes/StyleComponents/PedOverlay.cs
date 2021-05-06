using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    public class PedOverlay : IPedVariation
    {
        string _name;

        [JsonConstructor]
        internal PedOverlay(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public int Count => GetCount();

        private int GetCount()
        {
            Enum.TryParse(_name, out PedOverlays temp);
            return GetNumHeadOverlayValues((int)temp);
        }

        public bool HasAnyVariations => true;

        public bool HasTextureVariations => false;

        public bool HasVariations => true;

        public int Index { get; set; }

        public int TextureCount => 1;

        public int TextureIndex { get; set; }

        public bool IsVariationValid(int index, int textureIndex = 0)
        {
            return true;
        }

        public bool SetVariation(int index, int textureIndex = 0)
        {
            Debug.WriteLine(Name);
            Enum.TryParse(_name, out PedOverlays temp);
            SetPedHeadOverlay(Game.PlayerPed.Handle, (int)temp, index, 1f);
            return true;
        }
    }
}
