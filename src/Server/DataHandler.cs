using MongoDB.Driver;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;

namespace OpenRP.Framework.Server
{
    public class DataHandler : ServerAccessor
    {
        public DatabaseContext Context { get; }

        public DataHandler(ServerMain server) : base(server)
        {
            Context = new DatabaseContext(Server.Settings["mongodb:url"], Server.Settings["mongodb:db"]);
        }

        private void Initialize()
        {
        }
    }
}