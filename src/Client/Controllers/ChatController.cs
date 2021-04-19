using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class ChatController : ClientAccessor
    {
        ClientMain _client;

        internal ChatController (ClientMain client) : base (client)
        {
            _client = client;

            _client.eventController.RegisterNuiEvent("POST_MESSAGE", new Action<dynamic>(OnAddMessage));
            _client.eventController.RegisterNuiEvent("RESET_FOCUS", new Action<dynamic>(OnResetFocus));

            _client.RegisterKeyBinding("ToggleChatModule", "(HUD) Open chat", "t", new Action(ToggleChatModule));
        }

        private void ToggleChatModule()
        {
            var eventName = "TOGGLE_CHAT_MODULE";
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName }));
            SetNuiFocus(true, false);
        }

        private void OnResetFocus(dynamic args)
        {
            SetNuiFocus(false, false);
        }

        private void OnAddMessage(dynamic args)
        {
            Debug.WriteLine($"Post message: {JsonConvert.SerializeObject(args)}");
            MessageBox.AddMessage(255, 196, 79, args.value);
        }
    }
}
