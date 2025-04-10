using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.DataAccess.Models.DepartmentModels;

namespace Demo.BusinessLogic.Factories
{
    public static class DepartmentFactory
    {
        public static DepartmentDto DepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
        }

        public static DepartmentDetailsDto DepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                IsDeleted = department.IsDeleted
            };
        }
        public static Department TofEntity(this CreateDepartmentDto dto)
        {
            return new Department()
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation
            };
        }
        public static Department TofEntity(this UpdateDepartmentDto dto)
        {
            return new Department()
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation
            };
        }
    }
}
