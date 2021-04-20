using MongoDB.Bson;

namespace OpenRP.Framework.Database
{
    /// <summary>
    /// Document interface
    /// </summary>
    public interface IDocument
    {
        ObjectId Id { get; set; }
    }
}