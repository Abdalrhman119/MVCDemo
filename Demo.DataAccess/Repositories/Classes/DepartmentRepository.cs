using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Repositories.Interface;

namespace Demo.DataAccess.Repositories.Classes
{
    public class DepartmentRepository(AppDbContext dbContext) :

        GenericRepository<Department>( dbContext),
        IDepartmentRepository
    {

    }
}
