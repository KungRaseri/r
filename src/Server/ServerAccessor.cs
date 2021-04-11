namespace Redacted.Framework.Server
{
    public class ServerAccessor
    {
        protected ServerMain Server { get; }
        public ServerAccessor(ServerMain server)
        {
            Server = server;
        }
    }
}