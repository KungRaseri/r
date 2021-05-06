using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
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

        public float Opacity { get; set; } = 0;

        public int PrimaryColor { get; set; } = -1;

        public int SecondaryColor { get; set; } = -1;

        public int ColorType => GetColorType();

        private int GetColorType()
        {
            if (Name == "FacialHair" || Name == "Eyebrows" || Name == "ChestHair")
                return 1;

            return 2;
        }

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
            Enum.TryParse(_name, out PedOverlays temp);
            SetPedHeadOverlay(Game.PlayerPed.Handle, (int)temp, index, Opacity);
            Index = (int)temp;

            if (PrimaryColor == -1)
                SetInitialColor();

            return true;
        }

        public void SetPrimaryColor(int index)
        {
            SetPedHeadOverlayColor(Game.PlayerPed.Handle, Index, ColorType, index, SecondaryColor);
            PrimaryColor = index;
        }

        public void SetSecondaryColor(int index)
        {
            SetPedHeadOverlayColor(Game.PlayerPed.Handle, Index, ColorType, PrimaryColor, index);
            SecondaryColor = index;
        }

        private void SetInitialColor()
        {
            PrimaryColor = 0;
            SecondaryColor = 0;
            SetPedHeadOverlayColor(Game.PlayerPed.Handle, Index, ColorType, PrimaryColor, SecondaryColor);
        }
    }
}
