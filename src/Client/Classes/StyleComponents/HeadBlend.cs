using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    internal class HeadBlend
    {
        public int Face1 { get; set; } = 0;
        public int Face2 { get; set; } = 0;
        public float FaceBlend { get; set; } = 0.5f;
        public int Skin1 { get; set; } = 0;
        public int Skin2 { get; set; } = 0;
        public float SkinBlend { get; set; } = 0.5f;
    }
}
