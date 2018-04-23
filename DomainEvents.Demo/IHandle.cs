namespace DomainEvents.Demo
    {
    public interface IHandle<in T> where T : IDomainEvent
        {
        void Handle(T domainEvent );
        }
    }