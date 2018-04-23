using System.Linq;
using DomainEvents.Demo.Data;
using DomainEvents.Demo.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DomainEvents.Demo.Controllers
{
    public class HomeController : Controller
        {
        private readonly MyDbContext _myDbContext;

        public HomeController(MyDbContext myDbContext)  
            {
            _myDbContext = myDbContext;
            }

        public IActionResult Index()
            {
        return View();
            }

        [HttpGet, Route("{id:int}/Edit")]
        public IActionResult EditEmployee(int id)
            {
            var emp = _myDbContext.Employees.FirstOrDefault(e => e.Id == id);
            return View(ToEmpDto(emp));
            }

    [HttpPost,Route("{id:int}/Edit")]
    public IActionResult EditEmployee(int id, [Bind("FName,LName")]EmpDto model)
        {
        var existingEmp = _myDbContext.Employees.FirstOrDefault(e => e.Id == id);
        if (existingEmp == null)
            return NotFound($"Could not find Employee with id = '{id}'");
        existingEmp.Update(model);
        _myDbContext.SaveChanges();
        return Ok("Employee has changed successfully");
        }

        public EmpDto ToEmpDto(Employee emp)
            {
            return new EmpDto
                {
                FName = emp.FirstName,
                LName = emp.LastName,
                Ssn = emp.Ssn,
                Dob = emp.DateOfBirth
                };
            }
    }
}
