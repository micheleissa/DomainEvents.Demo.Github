using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEvents.Demo.Data.Entities;


namespace DomainEvents.Demo
{
    public class EmployeeChangedDomainEvent : IDomainEvent
    {
    public Employee ChangedEmployee { get; set; }
    }
}
