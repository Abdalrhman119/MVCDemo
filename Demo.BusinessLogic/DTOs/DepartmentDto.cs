using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOs
{
    public class DepartmentDto
    {
        public int DeptId { get; set; }
        public string Name { get; set; } = null!;
        public int Code { get; set; }
        public string? Description { get; set; }

        public DateTime? DateOfCreation { get; set; }

    }
}
