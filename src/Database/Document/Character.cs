using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenRP.Framework.Database.Document
{
    public class Character : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public dynamic Customization { get; set; }
    }
}
