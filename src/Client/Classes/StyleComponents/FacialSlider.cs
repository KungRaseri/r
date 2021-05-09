using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    public class FacialSlider : IPedVariation
    {
        string _name;

        [JsonConstructor]
        internal FacialSlider(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public float Value { get; set; } = 0f;

        public int Count => 0;

        public bool HasAnyVariations => true;

        public bool HasTextureVariations => false;

        public bool HasVariations => true;

        public int Index { get; set; }

        public int TextureCount => 0;

        public int TextureIndex { get; set; }

        public bool IsVariationValid(int index, int textureIndex = 0)
        {
            return true;
        }

        public bool SetVariation(int index, int textureIndex = 0)
        {
            return true;
        }

        public void SetFeature(float value)
        {
            Enum.TryParse(_name, out FacialSliders index);
            SetPedFaceFeature(Game.PlayerPed.Handle, (int)index, value);
        }
    }
}
