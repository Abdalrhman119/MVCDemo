using Demo.DataAccess.Models.Shared;
using Microsoft.IdentityModel.Tokens;

namespace Demo.DataAccess.Models.DepartmentModels
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
