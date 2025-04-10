using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        public bool DeleteDepartment(int id);
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int? UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);

    }
}