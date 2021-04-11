using Database;
using Database.Document;

namespace Redacted.Framework.Server
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