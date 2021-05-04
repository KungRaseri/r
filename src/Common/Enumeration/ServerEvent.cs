using OpenRP.Framework.Common.Attributes;

namespace OpenRP.Framework.Common.Enumeration
{
    /// <summary>
    /// Enum to house Server Event names.
    /// </summary>
    [Resource("framework")]
    public enum ServerEvent
    {
        /// <summary>
        /// Validates inputs from the MessageBox.
        /// </summary>
        COMMAND_VALIDATE,

        /// <summary>
        /// Passes the vehicle engine state to the server.
        /// </summary>
        STORE_ENGINE_STATE,

        SAVE_NEW_CHARACTER
    }
}
