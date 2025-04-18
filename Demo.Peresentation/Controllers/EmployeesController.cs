using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using Demo.Peresentation.ViewModels.EmployeesviewModels;
using Demo.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Peresentation.Controllers
{
    public class EmployeesController (IEmployeeService _employeeService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }
        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeviewModel EmployeeVM)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var createdEmployeeDto=new CreateEmployeeDto() {
                        Name = EmployeeVM.Name,
                        Address = EmployeeVM.Address,
                        Age = EmployeeVM.Age,   
                        DepartmentId = EmployeeVM.DepartmentId,
                        Email = EmployeeVM.Email,   
                        Gender = EmployeeVM.Gender,
                        HiringDate = EmployeeVM.HiringDate,
                        EmployeeType = EmployeeVM.EmployeeType,
                        IsActive = EmployeeVM.IsActive,
                        PhoneNumber = EmployeeVM.PhoneNumber,
                        Salary = EmployeeVM.Salary,
                        Image = EmployeeVM.Image
                    };
                    int res = _employeeService.CreateEmployee(createdEmployeeDto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error in creating Employee");
                    }
                }
                catch (Exception ex)
                {

                    if (_env.IsDevelopment())
                    {
                        ModelState.AddModelError(String.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }

                }

            }
            return View(EmployeeVM);
        }
        #endregion
        #region Details Of Employee

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null ? NotFound():View(  employee);
        }
        #endregion
        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null) return NotFound();

            var employeeVM = new EmployeeviewModel()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                DepartmentId = employee.DepartmentId
            };
            //ViewData["Departments"] = _departmentService.GetAllDepartments();

            return View(employeeVM); 
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeviewModel employeeVM)
        {
            if (!ModelState.IsValid) return View(employeeVM);
            try
            {
                var employeeDto = new UpdateEmployeeDto()
                {
                    Id = id,
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    EmployeeType = employeeVM.EmployeeType,
                    HiringDate = employeeVM.HiringDate,
                    Gender = employeeVM.Gender,
                    DepartmentId = employeeVM.DepartmentId
                };
                var res = _employeeService.UpdateEmployee(employeeDto);

                if (res.HasValue && res.Value > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Error in updating Employee");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(employeeVM);
        }


        #endregion
        #region Delete Employee

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();

        //    var employee = _employeeService.GetEmployeeById(id.Value);
        //    if (employee == null) return NotFound();

        //    return View(employee);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            try
            {
                var isDeleted = _employeeService.DeleteEmployee(id);

                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(String.Empty, "Employee can't be deleted");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(nameof(Delete), new { id });
        }

        #endregion
    }
}
