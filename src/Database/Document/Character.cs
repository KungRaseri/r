using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenRP.Framework.Database.Document
{
    class Character : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public BsonDateTime Dob { get; set; }
    }
}
