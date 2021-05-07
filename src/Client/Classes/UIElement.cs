using Newtonsoft.Json;
using System.Diagnostics;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    /// <summary>
    /// Provides handling of NUI elements.
    /// </summary>
    public static class UIElement
    {
        /// <summary>
        /// Toggle NUI element visibilty and control.
        /// </summary>
        /// <param name="eventName">The event name that handles the NUI element's visibility.</param>
        /// <param name="visible">The visibility of the NUI element.</param>
        /// <param name="focus">If focus should be given to the NUI element.</param>
        /// <param name="cursor">If focusing on the NUI element should also provide a cursor.</param>
        /// <param name="control">If the player should also maintain control of the game while focused.</param>
        public static void ToggleNuiModule(string eventName, bool visible, bool focus = false, bool cursor = false, bool control = false)
        {
            SendNuiMessage(JsonConvert.SerializeObject(new { eventName, visible }));
            SetNuiFocus(focus, cursor);
            SetNuiFocusKeepInput(control);
        }

        /// <summary>
        /// Resets the focus from NUI elements back to the game client.
        /// </summary>
        public static void ResetFocus()
        {
            SetNuiFocus(false, false);
        }
    }
}
