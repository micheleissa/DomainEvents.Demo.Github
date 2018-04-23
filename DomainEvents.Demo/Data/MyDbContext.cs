using System.Threading;
using System.Threading.Tasks;
using DomainEvents.Demo.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Demo.Data
{
    public class MyDbContext : DbContext
        {
        private readonly IMediator _mediator;
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
            {
            
            }

        public MyDbContext(DbContextOptions<MyDbContext> options, IMediator mediator) : base(options)
            {
            _mediator = mediator;
            }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Log> Logs { get; set; }

        public override int SaveChanges()
            {
            DomainEvents.GetDomainEventsQueue().DispatchAll();
            return base.SaveChanges();
            }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
            }
    }
}
