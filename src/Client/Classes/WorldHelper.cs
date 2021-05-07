using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public static class WorldHelper
    {
        public static Vector3 PosOffset(Vector3 position, float heading, float distance)
        {
            var radHeading = ToRadians(heading + 90);
            var x = distance * (float)Math.Cos(radHeading);
            var y = distance * (float)Math.Sin(radHeading);

            return new Vector3(position.X + x, position.Y + y, position.Z);
        }

        private static double ToRadians(float degree)
        {
            return (Math.PI / 180) * degree;
        }

        public static async Task FadeOut(int duration)
        {
            DoScreenFadeOut(duration);

            while (IsScreenFadingOut())
                await BaseScript.Delay(50);
        }

        public static async Task FadeIn(int duration)
        {
            DoScreenFadeIn(duration);

            while (IsScreenFadingIn())
                await BaseScript.Delay(50);
        }

        public static async Task DisableAllControls()
        {
            Game.DisableAllControlsThisFrame((int)InputMode.MouseAndKeyboard);
        }

        public static async Task RequestAnimationDictionary(string animDict)
        {
            while (!HasAnimDictLoaded(animDict))
            {
                RequestAnimDict(animDict);
                await BaseScript.Delay(50);
            }
        }

        public static toObject GetState<toObject>(Entity ent, string state)
        {
            toObject comp = JsonConvert.DeserializeObject<toObject>(JsonConvert.SerializeObject(ent.State.Get(state)));
            return comp;
        }
    }
}
