using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// Class that contains server command definitions.
    /// </summary>
    public class ServerCommand
    {
        /// <summary>
        /// Class that contains server command definitions.
        /// </summary>
        /// <param name="name">The name of the command. Can only contain lowercase letters.</param>
        /// <param name="command">A method that is called when the command is triggered.</param>
        /// <param name="help">A description of what the command does.</param>
        /// <param name="args">A list of descriptions for each argument passed with the command.</param>
        public ServerCommand(Action<int> command, string help, List<string> args)
        {
            Command = command;
            Help = help;
            Args = args;
        }

        /// <summary>
        /// The method called when executing the command.
        /// </summary>
        public Action<int> Command { get; }

        /// <summary>
        /// Description of the command.
        /// </summary>
        public string Help { get; }

        /// <summary>
        /// Argument descriptions.
        /// </summary>
        public List<string> Args { get; }
    }
}
