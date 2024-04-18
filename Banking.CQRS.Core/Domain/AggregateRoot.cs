using Banking.CQRS.Core.Events;

namespace Banking.CQRS.Core.Domain
{
    public abstract class AggregateRoot
    {
        private int version = -1;
        public string Id { get; set; } = string.Empty;

        List<BaseEvent> changes = new List<BaseEvent>();

        public int GetVersion()
        {
            return version;
        }

        public void SetVersion(int version)
        {
            this.version = version;
        }

        public List<BaseEvent> GetUncommitedChanges()
        {
            return changes;
        }

        public void MarkChangesAsCommited()
        {
            changes.Clear();
        }

        public void ApplyChange(BaseEvent @event, bool isNewEvent)
        {
            try
            {
                var ClaseDeEvento = @event.GetType();
                var method = GetType().GetMethod("Apply", new[] { ClaseDeEvento });
                method.Invoke(this, new object[] { @event });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (isNewEvent)
                {
                    changes.Add(@event);
                }
            }
        }

        public void RaiseEvent(BaseEvent @event)
        {
            ApplyChange(@event, true);
        }

        public void ReplaceEvents(IEnumerable<BaseEvent> events)
        {
            foreach (BaseEvent @event in events)
            {
                ApplyChange(@event, false);
            }
        }
    }
}
