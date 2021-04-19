using MongoDB.Driver;
using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;

namespace OpenRP.Framework.Server
{
    public class DataHandler : ServerAccessor
    {
        public DocumentRepository<Account> Accounts { get; }

        public DataHandler(ServerMain server, IMongoDatabase db) : base(server)
        {
            Initialize(db);
        }

        private void Initialize(IMongoDatabase db)
        {
            Accounts = new DocumentRepository<Account>(db);
        }
    }
}