using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEvents.Demo
{
    public class EmpChangedEventHandler : IHandle<EmployeeChangedDomainEvent>
    {
    public void Handle(EmployeeChangedDomainEvent domainEvent)
        {
        //do something here
        }
    }
}
