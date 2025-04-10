using Demo.DataAccess.Contexts;
using Demo.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext),
IEmployeeRepository
    {
       
    }

}
