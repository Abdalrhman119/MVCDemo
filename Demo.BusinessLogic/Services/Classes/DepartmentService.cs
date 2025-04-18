using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interface;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _uniteOfWork) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _uniteOfWork.DepartmentRepository.GetAll();
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
            var department = _uniteOfWork.DepartmentRepository.GetById(id);
            return department is null ? null : new DepartmentDetailsDto(department);
        }

        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
             _uniteOfWork.DepartmentRepository.Add(createDepartmentDto.TofEntity());

            return _uniteOfWork.SaveChanges();        }

        public int? UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var dept = updateDepartmentDto.TofEntity();
             _uniteOfWork.DepartmentRepository.Update(dept);

            return _uniteOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _uniteOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;

                 _uniteOfWork.DepartmentRepository.Remove(department);
            return _uniteOfWork.SaveChanges() > 0?true:false;


        }
    }
}
