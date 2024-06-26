﻿
using Banking.Account.Command.Domain.Common;
using Banking.CQRS.Core.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace Banking.Account.Command.Domain
{
    [BsonCollection("eventStore")]
    public class EventModel : Document
    {
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("aggregateIdentifier")]
        public string AggregateIdentifier { get; set; } = string.Empty;

        [BsonElement("aggregateType")]
        public string AggregateType { get; set; } = string.Empty;

        [BsonElement("version")]
        public int Version { get; set; }

        [BsonElement("eventType")]
        public string EventTpe { get; set; } = string.Empty;

        [BsonElement("eventData")]
        public BaseEvent? EventData { get; set; }
    }
}
