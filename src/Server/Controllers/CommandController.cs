using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using OpenRP.Framework.Common.Classes;
using System.Linq;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// Controller object that handles server commands.
    /// </summary>
    public sealed class CommandController : ServerAccessor
    {
        ServerMain _server;

        internal Dictionary<string, ServerCommand> Commands { get; private set; }

        internal CommandController(ServerMain server) : base(server)
        {
            _server = server;
            Commands = new Dictionary<string, ServerCommand>();

            _server.Event.RegisterEvent("COMMAND_VALIDATE", new Action<Player, string>(OnCommandValidate));
        }

        /// <summary>
        /// Registers a server command.
        /// </summary>
        /// <param name="name">The name of the command. Can only contain lowercase letters.</param>
        /// <param name="command">A method that is called when the command is triggered.</param>
        /// <param name="help">A description of what the command does.</param>
        /// <param name="args">A list of descriptions for each argument passed with the command.</param>
        public void Register(string name, MulticastDelegate command, string help, List<string> args)
        {
            var match = new Regex(@"^[a-z]+$"); // Must only contain lowercase letters
            if (!match.IsMatch(name))
            {
                throw new FormatException($"Cannot register command, invalid command name: {name}");
            }
            else
            {
                var Command = new ServerCommand(command, help, args);
                Commands.Add($"/{name}", Command);
            }
        }

        /// <summary>
        /// Unregisters a server command.
        /// </summary>
        /// <param name="name">The name of the command to unregister.</param>
        public void Unregister(string name)
        {
            if (!Commands.ContainsKey(name))
                throw new IndexOutOfRangeException($"Command does not exist: {name}");
            else
                Commands.Remove(name);
        }
        private void OnCommandValidate([FromSource] Player player, string input)
        {
            var split = input.Split(' ');
            var command = split[0];
            var args = split.ToList();
            args.RemoveAt(0);
            var commands = _server.Command.Commands;

            if (!commands.ContainsKey(command))
                throw new IndexOutOfRangeException("Command not found");
            else
                commands[command].Command.DynamicInvoke(player.Handle, args);
        }
    }
}
