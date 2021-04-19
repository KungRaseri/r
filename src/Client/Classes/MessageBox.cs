using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;
using System.Dynamic;

namespace OpenRP.Framework.Client.Controllers
{
    public static class MessageBox
    {
        const int Alpha = (int)(255 * 0.65);

        public static void AddMessage(int r, int g, int b, string message)
        {
            var color = GetBgColor(new { r, g, b });
            var eventName = "ADD_MESSAGE";
            var messageData = new { color, message };
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName, messageData }));
        }

        private static string GetBgColor(dynamic color)
        {
            var hex = string.Format($"#{color.r:X2}{color.g:X2}{color.b:X2}{Alpha:X2}");
            Debug.WriteLine(hex);
            return hex;
        }
    }
}
