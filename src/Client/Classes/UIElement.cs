﻿using Newtonsoft.Json;
using System.Diagnostics;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public static class UIElement
    {
        public static void ToggleNuiModule(string eventName, bool visible, bool focus, bool cursor)
        {
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName, visible }));

            if (focus)
                SetNuiFocus(true, cursor);
        }

        public static void ResetFocus()
        {
            SetNuiFocus(false, false);
        }
    }
}
