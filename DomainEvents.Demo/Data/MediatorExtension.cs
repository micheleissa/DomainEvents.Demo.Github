using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEvents.Demo.Data.Entities;
using MediatR;

namespace DomainEvents.Demo.Data
    {
    public static class MediatorExtension
        {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, MyDbContext ctx)
            {
            var domainEntities = ctx.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => { await mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);
            //We can also serialize our events and have out-of-band process pick those up.
            }
        }
    }
