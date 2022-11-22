using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Enums;

namespace MovieWebsite.Controllers
{
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        // GET: RoleManagerController
        public async Task<IActionResult> Index(bool? result)
        {
            List<IdentityRole> roles=new();
            try
            {
                roles = await _roleManager.Roles.ToListAsync();
                ViewData["CreateResult"] = result;
                if (result == true)
                {
                    ViewBag.Message = "Thao tác thành công!";
                }
                if(result == false)
                {
                    ViewBag.Message = "Error: Không thể để role trống";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Error:"+ ex.Message;
            }
            return View(roles);
        }

        // GET: RoleManagerController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        // GET: RoleManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string roleName)
        {
            bool result = false;
            if (roleName!=null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
                result = true;
            }
            return RedirectToAction(nameof(Index), new { result });
        }

        // GET: RoleManagerController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        // POST: RoleManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityRole roleNew)
        {
            if (ModelState.IsValid)
            {
                var role=await _roleManager.FindByIdAsync(id);
                role.Name = roleNew.Name;
                role.ConcurrencyStamp = roleNew.ConcurrencyStamp;
                role.NormalizedName = roleNew.Name.Normalize();
                var result = await _roleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Index), new { result = result.Succeeded });
            }
            return View();
        }

        // GET: RoleManagerController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        // POST: RoleManagerController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                var result=await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index), new { result = result.Succeeded });
            }
            catch
            {
                return View();
            }
        }
    }
}
