using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
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
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }







        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createdEmployeeDto)
        {

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(createdEmployeeDto);
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

            var updateEmployeeDto = new UpdateEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender)
            };

            return View(updateEmployeeDto); 
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateEmployeeDto);
            }

            try
            {
                int? res = _employeeService.UpdateEmployee(updateEmployeeDto);

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

            return View(updateEmployeeDto); 
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
