using Banking.CQRS.Core.Messages;

namespace Banking.CQRS.Core.Events
{
    public class BaseEvent : Message
    {
        public int Version { get; set; }

        public BaseEvent(string id) : base(id)
        {
        }
    }
}
