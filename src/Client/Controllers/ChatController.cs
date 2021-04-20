using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class ChatController : ClientAccessor
    {
        ClientMain _client;

        internal ChatController(ClientMain client) : base(client)
        {
            _client = client;

            _client.Event.RegisterNuiEvent("POST_MESSAGE", new Action<dynamic>(OnPostMessage));
            _client.Event.RegisterNuiEvent("RESET_FOCUS", new Action<dynamic>(OnResetFocus));
            _client.Event.RegisterEvent("ADD_MESSAGE", new Action<int, int, int, string>(AddMessage));

            _client.RegisterKeyBinding("ToggleChatModule", "(HUD) Open chat", "t", new Action(ToggleChatModule));
        }

        private void AddMessage(int r, int g, int b, string message)
        {
            MessageBox.AddMessage(r, g, b, message);
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

        private void OnPostMessage(dynamic args)
        {
            BaseScript.TriggerServerEvent("COMMAND_VALIDATE", args.value);
        }
    }
}
