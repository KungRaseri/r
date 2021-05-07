using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    internal class HeadBlend
    {
        int _face1 = 0;
        int _face2 = 0;
        float _faceBlend = 0.5f;
        int _skin1 = 0;
        int _skin2 = 0;
        float _skinBlend = 0.5f;

        public void SetFace(int primary, int secondary, float mix)
        {
            _face1 = primary;
            _face2 = secondary;
            _faceBlend = mix;
            SetFaceBlend();
        }

        public void SetSkin(int primary, int secondary, float mix)
        {
            _skin1 = primary;
            _skin2 = secondary;
            _skinBlend = mix;
            SetFaceBlend();
        }

        public void SetFaceBlend()
        {
            SetPedHeadBlendData(Game.PlayerPed.Handle, _face1, _face2, 0, _skin1, _skin2, 0, _faceBlend, _skinBlend, 0, false);
        }
    }
}
