using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Models.User;
using System.Text.Encodings.Web;
using System.Text;
using System.ComponentModel;

namespace MovieWebsite.Controllers
{
    public class UserRolesController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;
        public UserRolesController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            IUserStore<ApplicationUser> userStore,
            IEmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender= emailSender;
        }
        public async Task<IActionResult> Index(string? searchString, string? roleName, string? message)
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email!;
                thisViewModel.UserName = user.UserName!;
                thisViewModel.Roles = await GetUserRoles(user);
                thisViewModel.EmailConfirm = user.EmailConfirmed;
                userRolesViewModel.Add(thisViewModel);
            }
            if (searchString != null)
            {
                userRolesViewModel = userRolesViewModel.Where(u => u.UserName.Contains(searchString)||u.Email.Contains(searchString)).ToList();
                ViewBag.SearchString=searchString;
            }
            if(roleName != null)
            {
                userRolesViewModel=userRolesViewModel.Where(u=>u.Roles.Contains(roleName)).ToList();
            }
            ViewBag.RoleSelectList=new SelectList(_roleManager.Roles,"Name","Name");
            ViewBag.Message=message;
            return View(userRolesViewModel);
        }
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name!
                };
                if (await _userManager.IsInRoleAsync(user, role.Name!))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction(nameof(Index), new {message="Edit user role successfully!"});
        }
        public async Task<IActionResult> AddAsync(UserInputModel userInput)
        {
            if (ModelState.IsValid)
            {
                string email = userInput.Email;
                string password = userInput.Password;
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user,Enums.Roles.Basic.ToString());
                    return RedirectToAction(nameof(Index), new {message="Added successfully!"});
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        [ActionName("SendConfirmedEmail")]
        public async Task<IActionResult> SendConfirmEmailAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code, returnUrl = Url.Content("~/") },
                protocol: Request.Scheme);
            string body = $"https://localhost:7224/Identity/Account/ConfirmEmail?userId={userId}&code={code}";
            await _emailSender.SendEmailAsync(
                user.Email!,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(body!)}'>clicking here</a>.\n" +
                $"Your dedault password: Pa$$w0rd");
            return RedirectToAction(nameof(Index), new {message=$"A confirm email has been generated and sent to {user.Email}" });
        }
        public IActionResult ActivateAccount(string userId) => RedirectToAction(nameof(ChangeAccountStatus), new {userId, emailConfirmed =true});
        public IActionResult DeActivateAccount(string userId) => RedirectToAction(nameof(ChangeAccountStatus), new {userId, emailConfirmed =false});
        public async Task<IActionResult> ChangeAccountStatus(string userId, bool emailConfirmed)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction(nameof(Index), new { message = "Error: Can't find the user" });
            }
            user.EmailConfirmed = emailConfirmed;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index), new { message = $"Change {user.Email} status to {emailConfirmed}" });
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
