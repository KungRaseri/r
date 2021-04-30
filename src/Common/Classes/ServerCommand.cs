using System;
using System.Collections.Generic;
using System.Reflection;
using OpenRP.Framework.Common.Attributes;

namespace OpenRP.Framework.Common.Classes
{
    internal sealed class ServerCommand
    {
        public MulticastDelegate Command { get; internal set; }
        private string _help;
        private List<string> _args;

        internal ServerCommand(MulticastDelegate command, string help, List<string> args)
        {
            Command = command;
            _help = help;
            _args = args;
        }

        internal static string GenerateEventName(Enum eventName)
        {
            var resource = eventName.GetType().GetCustomAttribute<ResourceAttribute>().Name;
            var name = $"{resource}:{eventName}";
            return name;
        }
    }
}
