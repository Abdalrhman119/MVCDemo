using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.DTOs
{
    public class DepartmentDetailsDto
    {
        public DepartmentDetailsDto()
        {
        }

        public DepartmentDetailsDto(Department department)
        {
            DeptId = department.Id;
            Name = department.Name;
            Code = department.Code;
            Description = department.Description;
            DateOfCreation = department.CreatedOn;
            CreatedBy = department.CreatedBy;
            IsDeleted = department.IsDeleted;
            LastModifiedBy = department.LastModifiedBy;
            LastModifiedOn = department.LastModifiedOn;

        }
        public int DeptId { get; set; }
        public string Name { get; set; } = null!;
        public int Code { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
