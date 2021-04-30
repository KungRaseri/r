using OpenRP.Framework.Common.Attributes;

namespace OpenRP.Framework.Common.Enumeration
{
    /// <summary>
    /// Enum to house Client Event names.
    /// </summary>
    [Resource("framework")]
    public enum ClientEvent
    {
        /// <summary>
        /// Executes the OpenRP.Framework.Client.Controllers.MessageBox.AddMessage() method.
        /// </summary>
        ADD_MESSAGE,

        /// <summary>
        /// Invokes the teleport command.
        /// </summary>
        COMMAND_TP,

        SEND_VEHILCE_STATE
    }
}
