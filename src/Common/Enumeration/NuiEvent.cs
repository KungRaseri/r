using OpenRP.Framework.Common.Attributes;

namespace OpenRP.Framework.Common.Enumeration
{
    [Resource("framework")]
    /// <summary>
    /// Enum to house NUI Event names.
    /// </summary>
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
    }
}
