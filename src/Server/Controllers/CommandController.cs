using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// A controller that handles server commands.
    /// </summary>
    public class CommandController : ServerAccessor
    {
        /// <summary>
        /// Holds all of the registered server commands.
        /// </summary>
        public Dictionary<string, ServerCommand> Commands { get; private set; }

        /// <summary>
        /// A controller that handles server commands.
        /// </summary>
        /// <param name="server">The server instance.</param>
        public CommandController(ServerMain server) : base(server)
        {
            Commands = new Dictionary<string, ServerCommand>();
        }

        /// <summary>
        /// Registers a server command.
        /// </summary>
        /// <param name="name">The name of the command. Can only contain lowercase letters.</param>
        /// <param name="command">A method that is called when the command is triggered.</param>
        /// <param name="help">A description of what the command does.</param>
        /// <param name="args">A list of descriptions for each argument passed with the command.</param>
        public void Register(string name, Action<int> command, string help, List<string> args)
        {
            var match = new Regex(@"^[a-z]+$"); // Must only contain lowercase letters

            try
            {
                if (!match.IsMatch(name))
                    throw new FormatException($"Cannot register command, invalid command name: {name}");

                var Command = new ServerCommand(command, help, args);
                Commands.Add(name, Command);
            }
            catch (FormatException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Unregisters a server command.
        /// </summary>
        /// <param name="name">The name of the command to unregister.</param>
        public void Unregister(string name)
        {
            try
            {
                if (!Commands.ContainsKey(name))
                {
                    throw new IndexOutOfRangeException($"Command does not exist: {name}");
                }
                else
                {
                    Commands.Remove(name);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
