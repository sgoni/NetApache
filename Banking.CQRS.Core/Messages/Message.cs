namespace Banking.CQRS.Core.Messages
{
    public abstract class Message
    {
        public string Id { get; set; } = string.Empty;

        public Message(string id)
        {
            Id = id;
        }
    }
}
