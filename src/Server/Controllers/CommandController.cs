using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using OpenRP.Framework.Server.Classes;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// Controller object that handles server commands.
    /// </summary>
    public sealed class CommandController : ServerAccessor
    {
        Dictionary<string, ServerCommand> _commands;

        internal CommandController(ServerMain server) : base(server)
        {
            _commands = new Dictionary<string, ServerCommand>();
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
            if (!match.IsMatch(name))
                throw new FormatException($"Cannot register command, invalid command name: {name}");

            var Command = new ServerCommand(command, help, args);
            _commands.Add(name, Command);
        }

        /// <summary>
        /// Unregisters a server command.
        /// </summary>
        /// <param name="name">The name of the command to unregister.</param>
        public void Unregister(string name)
        {
            if (!_commands.ContainsKey(name))
                throw new IndexOutOfRangeException($"Command does not exist: {name}");
            else
                _commands.Remove(name);
        }
    }
}
