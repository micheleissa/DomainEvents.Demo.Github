using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEvents.Demo.Data;
using DomainEvents.Demo.Data.Entities;
using DomainEvents.Demo.Repositories;

namespace DomainEvents.Demo
{
    public class EmpChangedEventHandler : IHandle<EmployeeChangedDomainEvent>
        {
        private readonly IEmpRepository _empRepository;
        private readonly MyDbContext _dbContext;

        public EmpChangedEventHandler(IEmpRepository empRepository, MyDbContext dbContext)
            {
            _empRepository = empRepository;
            _dbContext = dbContext;
            }

        public void Handle(EmployeeChangedDomainEvent domainEvent)
        {
        //do something here
        var emp = _empRepository.FindById(domainEvent.EmpId);
        var log = new Log
            {
            Message = $"Emp: {emp.FirstName} - {emp.LastName} has changed"
            };
        _dbContext.Logs.Add(log);
        }
    }
}
