using System;
using System.Linq;
using DomainEvents.Demo.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace DomainEvents.Demo.Test
{
    public class EmployeeTests
    {
        [Fact]
        public void Employee_Update_Should_Trigger_Changed_Event()
            {
            var serviceProvider = new Mock<IServiceProvider>();
            var serviceScope = new Mock<IServiceScope>();
            serviceProvider.Setup(x => x.GetService(typeof(DomainEvents)))
                           .Returns(new DomainEvents());

            serviceScope.Setup(x => x.ServiceProvider)
                        .Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope())
                               .Returns(serviceScope.Object);

            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                           .Returns(serviceScopeFactory.Object);

            DomainEvents.ServiceProvider = serviceProvider.Object;

            var emp = new Employee
                {
                FirstName = "John",
                LastName = "Doe",
                Ssn = "123-45-6789",
                DateOfBirth = DateTime.Now.AddYears(-25)
                };
            var changedDto = new EmpDto
                {
                FName = "Will",
                LName = "Smith",
                Ssn = "987-65-4321",
                Dob = DateTime.Now.AddYears(-21)
            };
            emp.Update(changedDto);
            
            Assert.NotEmpty(DomainEvents.GetDomainEventsQueue().DomainEventsQueue);
            var e = DomainEvents.GetDomainEventsQueue().DomainEventsQueue.First() as EmployeeChangedDomainEvent;
            Assert.NotNull(e);
            }
    }
}
