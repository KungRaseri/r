using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Common.Enumeration;
using System;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    /// <summary>
    /// Handles the message box.
    /// </summary>
    public class ChatController : ClientAccessor
    {
        const int Alpha = (int)(255 * 0.8);

        internal ChatController(ClientMain client) : base(client)
        {
            Client.Event.RegisterNuiEvent(NuiEvent.POST_MESSAGE, new Action<dynamic>(OnPostMessage));
            Client.Event.RegisterNuiEvent(NuiEvent.RESET_FOCUS, new Action<dynamic>(OnResetFocus));
            Client.Event.RegisterEvent(ClientEvent.ADD_MESSAGE, new Action<int, int, int, string>(AddMessage));

            Client.RegisterKeyBinding("ToggleChatModule", "(HUD) Open chat", "t", new Action(ToggleChatModule));
        }

        private void ToggleChatModule()
        {
            var eventName = "TOGGLE_CHAT_MODULE";
            UIElement.ToggleNuiModule(eventName, true, true, false);
        }

        private void OnResetFocus(dynamic args)
        {
            UIElement.ResetFocus();
        }

        private void OnPostMessage(dynamic args)
        {
            Debug.WriteLine($"Posting message: {ServerEvent.COMMAND_VALIDATE.GetType().FullName}");
            Client.Event.TriggerServerEvent(ServerEvent.COMMAND_VALIDATE, args.value);
        }

        /// <summary>
        /// Adds a new message bubble to the Message Box.
        /// </summary>
        /// <param name="r">Red value of the background color.</param>
        /// <param name="g">Green value of the background color.</param>
        /// <param name="b">Blue value of the background color.</param>
        /// <param name="message">The message to display in the message bubble.</param>
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
            return hex;
        }
    }
}
