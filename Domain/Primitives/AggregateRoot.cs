namespace Domain.Primitives;

public abstract class AggregateRoot {

    private readonly List<DomainEvent> domainEventsLst = new();

    public ICollection<DomainEvent> GetDomainEvents() => domainEventsLst;

    protected void Raise(DomainEvent domainEvent) {
        domainEventsLst.Add(domainEvent);
    }
}