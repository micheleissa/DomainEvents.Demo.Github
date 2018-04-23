using System;

namespace DomainEvents.Demo.Data.Entities
{
    public class Employee : EntityBase<int>
        {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public void Update(EmpDto emailDto)
            {
            this.FirstName = emailDto.FName;
            this.LastName = emailDto.LName;
            this.Ssn = emailDto.Ssn;
            this.DateOfBirth = emailDto.Dob;
            var changedEvent = new EmployeeChangedDomainEvent
                {
                EmpId = this.Id // just to demonstrate
                };
            AddDomainEvent(changedEvent);
            //DomainEvents.GetDomainEventsQueue().AddToQueue(changedEvent);
            }
        }
}
