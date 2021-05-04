using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

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
        [BsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
