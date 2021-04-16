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
        public static void AddMessage(string color, string message)
        {
            var eventName = "ADD_MESSAGE";
            var messageData = new { color, message };
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName, messageData }));
        }
    }
}
