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

        RECEIVE_VEHICLE_STATE
    }
}
