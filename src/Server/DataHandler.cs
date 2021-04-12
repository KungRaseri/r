using OpenRP.Framework.Database;
using OpenRP.Framework.Database.Document;

namespace OpenRP.Framework.Server
{
    public class DataHandler : ServerAccessor
    {
        public DocumentRepository<Account> Accounts { get; }

        public DataHandler(ServerMain server)
        : base(server)
        {
            Accounts = new DocumentRepository<Account>();
        }
    }
}