using System;
using System.Collections.Generic;

namespace OpenRP.Framework.Common.Classes
{
    internal sealed class ServerCommand
    {
        public MulticastDelegate Command { get; internal set; }
        string _help;
        List<string> _args;

        internal ServerCommand(MulticastDelegate command, string help, List<string> args)
        {
            Command = command;
            _help = help;
            _args = args;
        }
    }
}
