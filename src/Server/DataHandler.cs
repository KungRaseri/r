using MongoDB.Driver;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;

namespace OpenRP.Framework.Server
{
    public class DataHandler : ServerAccessor
    {
        public DatabaseContext Context { get; }

        public DataHandler(ServerMain server, string connString, string database) : base(server)
        {
            Context = new DatabaseContext(connString, database);
        }

        private void Initialize()
        {
        }
    }
}