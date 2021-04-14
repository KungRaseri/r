using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenRP.Framework.Server.Controllers
{
    internal sealed class ServerCommand
    {
        Action<int> _command;
        string _help;
        List<string> _args;

        internal ServerCommand(Action<int> command, string help, List<string> args)
        {
            _command = command;
            _help = help;
            _args = args;
        }
    }
}
