namespace OpenRP.Framework.Common.Enumeration
{
    /// <summary>
    /// Enum to house Client Event names
    /// </summary>
    public enum ClientEvent
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
        /// Executes the OpenRP.Framework.Client.Controllers.MessageBox.AddMessage() method.
        /// </summary>
        ADD_MESSAGE,

        /// <summary>
        /// 
        /// </summary>
        COMMAND_TP,
    }
}
