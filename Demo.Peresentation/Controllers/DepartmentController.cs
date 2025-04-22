using Demo.BusinessLogic.DTOs;
using Demo.BusinessLogic.DTOs.DepartmentDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Peresentation.ViewModels.DepartmentsviewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    [Authorize]

    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _env) : Controller

    {
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var CreateDepartmentDto = new CreateDepartmentDto()
                    {
                        Name = departmentVM.Name,
                        Code = departmentVM.Code,
                        Description = departmentVM.Description,
                        DateOfCreation = departmentVM.DateOfCreation
                    };
                    int res = _departmentService.CreateDepartment(CreateDepartmentDto);
                    var message = string.Empty; 
                    if (res > 0) message = "Department Created Successfully";
                    else message = ("Error in creating department");
                    ViewData["SpecialMsg01"] = message;  
                    TempData["SpecialMsg02"] = message/*new CreateDepartmentDto() { Description=message}*/;
                    return RedirectToAction(nameof(Index)); 
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
            return View(departmentVM);
        }
        #endregion


        #region Details
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }
        #endregion


        #region Edit   
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();
            else
            {
                var departmentViewModel = new DepartmentViewModel()
                {
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    DateOfCreation = department.DateOfCreation

                };
                return View(departmentViewModel);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentEditViewModel)
        {
            if (!ModelState.IsValid) return View(departmentEditViewModel);
            try
            {
                var updatedDept = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = departmentEditViewModel.Code,
                    Name = departmentEditViewModel.Name,
                    Description = departmentEditViewModel.Description,
                    DateOfCreation = departmentEditViewModel.DateOfCreation
                };
                var res = _departmentService.UpdateDepartment(updatedDept);
                if (res > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(String.Empty, "Department Can't Be Updated");
                    //return View(departmentEditViewModel);
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
            return View(departmentEditViewModel);
        }
        #endregion


        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(String.Empty, "Department Can't Be Deleted");
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