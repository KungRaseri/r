using OpenRP.Framework.Common.Attributes;

namespace OpenRP.Framework.Common.Enumeration
{
    /// <summary>
    /// Enum to house NUI Event names.
    /// </summary>
    [Resource("framework")]
    public enum NuiEvent
    {
        /// <summary>
        /// Triggers server event COMMAND_VALIDATE when the Message Box module posts user input.
        /// </summary>
        POST_MESSAGE,

        /// <summary>
        /// Returns focus from NUI back to the game.
        /// </summary>
        RESET_FOCUS,

        /// <summary>
        /// Handles toggling of various vehicle components.
        /// </summary>
        TOGGLE_VEHICLE_COMPONENT,

        SAVE_NEW_CHARACTER,

        SET_CHARACTER_MODEL,

        AGGREGATE_DATA,

        SET_PED_COMPONENT,

        SET_COMPONENT_COLOR
    }
}
