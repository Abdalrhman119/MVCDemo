using Demo.DataAccess.Contexts;

namespace Demo.DataAccess.Repositories
{
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    {
        private readonly AppDbContext _DbContext = dbContext;



        //CRUD Operations
        //GetAll
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _DbContext.Departments.ToList();
            else
                return _DbContext.Departments.AsNoTracking().ToList();
        }
        //GetById
        public Department GetById(int id)
        {
            return _DbContext.Departments.Find(id);
        }
        //Add
        public int Add(Department department)
        {
            _DbContext.Departments.Add(department);
            return _DbContext.SaveChanges();
        }
        //Edit
        public int Update(Department department)
        {
            _DbContext.Departments.Update(department);
            return _DbContext.SaveChanges();
        }
        //Delete
        public int Remove(Department department)
        {
            _DbContext.Departments.Remove(department);
            return _DbContext.SaveChanges();
        }
    }
}
