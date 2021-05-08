using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    internal class HeadBlend
    {
        public int Face1 { get; private set; } = 0;
        public int Face2 { get; private set; } = 0;
        public float FaceBlend { get; private set; } = 0.5f;
        public int Skin1 { get; private set; } = 0;
        public int Skin2 { get; private set; } = 0;
        public float SkinBlend { get; private set; } = 0.5f;

        public void SetFace(int primary, int secondary, float mix)
        {
            Face1 = primary;
            Face2 = secondary;
            FaceBlend = mix;
            SetFaceBlend();
        }

        public void SetSkin(int primary, int secondary, float mix)
        {
            Skin1 = primary;
            Skin2 = secondary;
            SkinBlend = mix;
            SetFaceBlend();
        }

        public void SetFaceBlend()
        {
            SetPedHeadBlendData(Game.PlayerPed.Handle, Face1, Face2, 0, Skin1, Skin2, 0, FaceBlend, SkinBlend, 0, false);
        }
    }
}
