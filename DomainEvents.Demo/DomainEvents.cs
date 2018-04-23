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

        public static IServiceProvider ServiceProvider
        {
            get { return CallContext<IServiceProvider>.GetData("DomainEvents.ServiceProvider"); }
            set { CallContext<IServiceProvider>.SetData("DomainEvents.ServiceProvider", value); }
        }

        public void AddToQueue<T>(T domainEvent) where T : IDomainEvent
        {
            _domainEventsQueue = _domainEventsQueue ?? new List<IDomainEvent>();
            _domainEventsQueue.Add(domainEvent);
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
                var handlers = ServiceProvider.GetServices<IHandle<T>>();

                foreach (var handler in handlers)
                {
                    handler.Handle(args);
                }
        }

        public static DomainEvents GetDomainEventsQueue()
            {
            var events = ServiceProvider.GetService<DomainEvents>();
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
