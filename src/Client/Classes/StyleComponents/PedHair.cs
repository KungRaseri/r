using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes.StyleComponents
{
    class PedHair
    {
        public int Primary { get; set; }

        public int Secondary { get; set; }

        public bool SetHair(int index, string target)
        {
            if (target == "primary")
            {
                SetPedHairColor(Game.PlayerPed.Handle, index, Secondary);
                return true;
            }
            else if (target == "secondary")
            {
                SetPedHairColor(Game.PlayerPed.Handle, Primary, index);
                return true;
            }

            return false;
        }
    }
}
