using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interface;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            var departmentsToReturn = departments.Select(d => new DepartmentDto()
            {
                DeptId = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = d.CreatedOn
            });
            return departmentsToReturn;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department is null ? null : new DepartmentDetailsDto(department);
        }

        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var res = _departmentRepository.Add(createDepartmentDto.TofEntity());

            return res;
        }

        public int? UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var dept = updateDepartmentDto.TofEntity();
            var res = _departmentRepository.Update(dept);

            return res;
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;

                var res = _departmentRepository.Remove(department);
            return res>0?true:false;


        }
    }
}
