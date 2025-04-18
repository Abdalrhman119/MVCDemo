using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
   public interface IEmployeeService
    {
        IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDto? GetEmployeeById(int id);

        public bool DeleteEmployee(int id);
        int CreateEmployee(CreateEmployeeDto createEmployeeDto);
        int? UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
    }
}
