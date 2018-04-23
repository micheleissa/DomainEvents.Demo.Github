using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEvents.Demo.Data.Entities;
using DomainEvents.Demo.Data;

namespace DomainEvents.Demo.Repositories
{
    public interface IEmpRepository
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> FindbyName(string search);
        Employee FindById(int id);
    }
    public class EmpRepository : IEmpRepository
    {
        private readonly MyDbContext _dbContext;
        public EmpRepository(MyDbContext ctx)
        {
            this._dbContext = ctx;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees;
        }

        public IEnumerable<Employee> FindbyName(string search)
        {
            return _dbContext.Employees.Where(e => e.FirstName.Contains(search));
        }

        public Employee FindById(int id)
            {
            return _dbContext.Employees.Find(id);
            }
    }
}
