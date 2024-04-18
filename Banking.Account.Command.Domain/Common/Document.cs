using MongoDB.Bson.Serialization.Attributes;

namespace Banking.Account.Command.Domain.Common
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public DateTime? CreatedDate => DateTime.Now;
    }
}
