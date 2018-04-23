using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DomainEvents.Demo
{
    public class DomainEvents
    {
        private List<IDomainEvent> _domainEventsQueue = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEventsQueue => _domainEventsQueue?.AsReadOnly();

        public static IServiceProvider AppServices { get; set; }

        public void AddToQueue<T>(T domainEvent) where T : IDomainEvent
        {
            _domainEventsQueue = _domainEventsQueue ?? new List<IDomainEvent>();
            _domainEventsQueue.Add(domainEvent);
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (AppServices != null)
            {
                var handlers = new List<IHandle<T>>();
                var scope = AppServices.CreateScope();
                handlers.AddRange(scope.ServiceProvider.GetServices<IHandle<T>>());

                foreach (var handler in handlers)
                {
                    handler.Handle(args);
                }
            }
        }

        public static DomainEvents GetDomainEventsQueue()
            {
            var scope = AppServices.CreateScope();
            var events = scope.ServiceProvider.GetService<DomainEvents>();
            return events;
        }

        public void DispatchAll()
        {
            while (_domainEventsQueue.Any())
            {
                var domainEvent = _domainEventsQueue[0];
                _domainEventsQueue.Remove(domainEvent);

                Raise((dynamic)domainEvent);
            }
        }
    }
}
