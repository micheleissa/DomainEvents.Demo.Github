using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEvents.Demo.Data.Entities
{
    public class Log : EntityBase<int>
    {
    public string Message { get; set; }
    public int Level { get; set; }
    }
}
