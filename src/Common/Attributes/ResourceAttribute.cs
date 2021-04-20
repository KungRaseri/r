using System;

namespace OpenRP.Framework.Common.Attributes
{
    public class ResourceAttribute : Attribute
    {
        public string Name { get; private set; }

        internal ResourceAttribute(string name)
        {
            Name = name;
        }
    }
}
