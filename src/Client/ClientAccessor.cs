namespace OpenRP.Framework.Client
{
    public class ClientAccessor
    {
        protected ClientMain Client { get; }

        public ClientAccessor(ClientMain client)
        {
            Client = client;
        }
    }
}
