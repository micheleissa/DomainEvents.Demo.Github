using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEvents.Demo.Data.Entities;
using MediatR;


namespace DomainEvents.Demo
{
    public class EmployeeChangedDomainEvent : INotification
    {
    public int EmpId { get; set; }
    }
}
