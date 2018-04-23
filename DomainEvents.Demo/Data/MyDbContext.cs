using DomainEvents.Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Demo.Data
{
    public class MyDbContext : DbContext
        {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
            {
            
            }

        public DbSet<Employee> Employees { get; set; }

        public override int SaveChanges()
            {
            DomainEvents.GetDomainEventsQueue().DispatchAll();
            return base.SaveChanges();
            }
        }
}
