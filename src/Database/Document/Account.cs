using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OpenRP.Framework.Common.Interface;

namespace OpenRP.Framework.Database.Document
{
    /// <summary>
    /// The Account document 
    /// </summary>
    public class Account : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string[] Identifiers { get; set; }
        public string[] Roles { get; set; }
    }
}
