﻿namespace Banking.CQRS.Core.Events
{
    public class AccountClosedEvent : BaseEvent
    {
        public AccountClosedEvent(string id) : base(id)
        {
        }
    }
}
