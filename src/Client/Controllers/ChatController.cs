using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRP.Framework.Client.Controllers
{
    public class ChatController : ClientAccessor
    {
        ClientMain _client;

        internal ChatController (ClientMain client) : base (client)
        {
            _client = client;

            _client.eventController.RegisterNuiEvent("POST_MESSAGE", new Action<dynamic>(OnAddMessage));
        }

        private void OnAddMessage(dynamic message)
        {
            Debug.WriteLine($"Post message: {JsonConvert.SerializeObject(message)}");
            MessageBox.AddMessage("blue", message.value);
        }
    }
}
