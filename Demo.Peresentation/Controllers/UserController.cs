using Demo.BusinessLogic.DTOs.UserRoleDtos;
using Demo.DataAccess.Models.IdintityModaels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Peresentation.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            var query = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(SearchInput))
            {
                query = query.Where(u => u.FirstName.ToLower().Contains(SearchInput.ToLower()));
            }

            var usersList = await query.ToListAsync();

            var users = new List<UserToReturnDto>();
            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                users.Add(new UserToReturnDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles
                });
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_UserTablePartial", users);
            }

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new { statusCode = 404, message = $"User With Id : {id} is not Found " });

            var roles = await _userManager.GetRolesAsync(user);

            var dto = new UserToReturnDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (id != model.Id)
                return BadRequest("Invalid Operation !!");

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return BadRequest("Invalid Operation !!");

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to delete user");
            return await Details(id, "Delete");
        }
    }
}
