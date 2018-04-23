using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEvents.Demo.Data.Entities;

namespace DomainEvents.Demo.Data
{
    public class DataSeeder
    {
    private readonly MyDbContext _context;

    public DataSeeder(MyDbContext context)
        {
        _context = context;
        }

    public async Task Seed()
        {
        _context.Database.EnsureCreated();
        var emp = new Employee
            {
            FirstName = "John",
            LastName = "Smith",
            Ssn = "111-11-1111",
            DateOfBirth = new DateTime(1980, 2, 1),
            Address = "123 Main St."
            };
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();
        }
    }
}
