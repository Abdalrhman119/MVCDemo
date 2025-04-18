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
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Demo.BusinessLogic.Services.AttachmentService;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _uniteOfWork,
        IMapper _mapper,
       IAttachmentService attachmentService  ) : IEmployeeService
    {
        private readonly IAttachmentService _AttachmentService = attachmentService;

        public IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees ;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName)) {
                employees = _uniteOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _uniteOfWork.EmployeeRepository.GetAll(entity => entity.Name.ToLower()
            .Contains(EmployeeSearchName.ToLower()));
            }
            return _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
//            var employee = _uniteOfWork.EmployeeRepository.GetAll(e=>new GetEmployeeDto()
//            {
//                Id = e.Id,
//                Name = e.Name,
//                Age = e.Age,
//            });
//return employee;
  }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var emp = _uniteOfWork.EmployeeRepository.GetById(id);
            return emp is null ? null : _mapper.Map<EmployeeDetailsDto>(emp);
        }

        public int CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(createEmployeeDto);
            var imageName = _AttachmentService.Upload(createEmployeeDto.Image,"Images");
            mappedEmployee.ImageName = imageName;
            _uniteOfWork.EmployeeRepository.Add(mappedEmployee);
            return _uniteOfWork.SaveChanges();

        }
        public int? UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(updateEmployeeDto);
             _uniteOfWork.EmployeeRepository.Update(mappedEmployee);
            return _uniteOfWork.SaveChanges();

        }
        public bool DeleteEmployee(int id)
        {
            var employee = _uniteOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            _uniteOfWork.EmployeeRepository.Remove(employee);
            return _uniteOfWork.SaveChanges()
> 0? true:false;

        }

        public bool CreatePurchase() {
            _uniteOfWork.SaveChanges();
            return true;    
        }

        public bool DeleteSale()
        {
            return true;
        }   


    }
}
