using CitizenFX.Core;
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
        PedOverlays _component;

        public int Count => throw new NotImplementedException();

        public bool HasAnyVariations => throw new NotImplementedException();

        public bool HasTextureVariations => throw new NotImplementedException();

        public bool HasVariations => throw new NotImplementedException();

        public int Index { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int TextureCount => throw new NotImplementedException();

        public int TextureIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsVariationValid(int index, int textureIndex = 0)
        {
            throw new NotImplementedException();
        }

        public bool SetVariation(int index, int textureIndex = 0)
        {
            throw new NotImplementedException();
        }
    }
}
