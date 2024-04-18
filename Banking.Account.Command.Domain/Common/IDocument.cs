using MongoDB.Bson.Serialization.Attributes;

namespace Banking.Account.Command.Domain.Common
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        string Id { get; set; }

        DateTime? CreatedDate { get; }
    }
}
