using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DomainEvents.Demo.Data;
using DomainEvents.Demo.Data.Entities;
using DomainEvents.Demo.Repositories;
using MediatR;

namespace DomainEvents.Demo
{
    public class EmpChangedEventHandler : INotificationHandler<EmployeeChangedDomainEvent>
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
       
        }

        public Task Handle(EmployeeChangedDomainEvent notification, CancellationToken cancellationToken)
            {
            var emp = _empRepository.FindById(notification.EmpId);
            var log = new Log
                {
                Message = $"Emp: {emp.FirstName} - {emp.LastName} has changed"
                };
            return _dbContext.Logs.AddAsync(log, cancellationToken);
            }
        }
}
