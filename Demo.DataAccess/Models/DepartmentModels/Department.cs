using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Models.DepartmentModels
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string? Description { get; set; }
    }
}
