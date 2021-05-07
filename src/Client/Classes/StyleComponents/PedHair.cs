using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    class PedHair
    {
        int _primary => GetPedHairColor(Game.PlayerPed.Handle);
        int _secondary => GetPedHairHighlightColor(Game.PlayerPed.Handle);

        public bool SetHair(int index, string target)
        {
            if (target == "primary")
            {
                SetPedHairColor(Game.PlayerPed.Handle, index, _secondary);
                return true;
            }
            else if (target == "secondary")
            {
                SetPedHairColor(Game.PlayerPed.Handle, _primary, index);
                return true;
            }

            return false;
        }
    }
}
