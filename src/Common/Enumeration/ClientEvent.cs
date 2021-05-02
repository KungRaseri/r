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

        /// <summary>
        /// Sends vehicle component states to the NUI panel.
        /// </summary>
        SEND_VEHILCE_STATE,

        /// <summary>
        /// Passes the vehicle engine state to the client.
        /// </summary>
        STORE_ENGINE_STATE
    }
}
