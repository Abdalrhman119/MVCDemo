using Demo.BusinessLogic.DTOs;
using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
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
        public IActionResult Create(CreateDepartmentDto dto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int res = _departmentService.CreateDepartment(dto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Error in creating department");
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
            return View(dto);
        }
        #endregion


        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }





    }
}