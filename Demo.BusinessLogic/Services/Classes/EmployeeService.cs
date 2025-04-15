using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository,IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<GetEmployeeDto> GetAllEmployees()
        {
            var emps = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<GetEmployeeDto>>(emps);
//            var employee = _employeeRepository.GetAll(e=>new GetEmployeeDto()
//            {
//                Id = e.Id,
//                Name = e.Name,
//                Age = e.Age,
//            });
//return employee;
  }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var emp = _employeeRepository.GetById(id);
            return emp is null ? null : _mapper.Map<EmployeeDetailsDto>(emp);
        }

        public int CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(createEmployeeDto);
            return _employeeRepository.Add(mappedEmployee);
           

        }
        public int? UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(updateEmployeeDto);
            return _employeeRepository.Update(mappedEmployee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            var res =_employeeRepository.Remove(employee);
            return res>0? true:false;

        }


    }
}
