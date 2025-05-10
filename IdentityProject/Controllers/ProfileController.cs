using IdentityProject.Context;
using IdentityProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly EmailContext _emailContext;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager, EmailContext emailContext)
        {
            _userManager = userManager;
            _emailContext = emailContext;
        }

        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Name = user.Name;
            ViewBag.Surname = user.Surname;
            ViewBag.Username = user.UserName;
            ViewBag.Email = user.Email;

            ViewBag.RecipientEmailAddressCount = _emailContext.Messages
                .Where(x => x.ReceiverEmail == user.Email)
                .Count();

            ViewBag.SenderEmailAddressCount = _emailContext.Messages
                .Where(x => x.SenderEmail == user.Email)
                .Count();

            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Name = values.Name;
            ViewBag.Surname = values.Surname;
            ViewBag.Email = values.Email;
            ViewBag.Username = values.UserName;
            ViewBag.RecipientEmailAddressCount = _emailContext.Messages
                                    .Where(x => x.ReceiverEmail == values.Email)
                                    .Count();

            ViewBag.SenderEmailAddressCount = _emailContext.Messages
                                                 .Where(x => x.SenderEmail == values.Email)
                                                 .Count();

            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppUser updatedUser, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;

            if (!string.IsNullOrEmpty(newPassword))
            {
                var hasPassword = await _userManager.HasPasswordAsync(user);
                if (hasPassword)
                {
                    var removeResult = await _userManager.RemovePasswordAsync(user);
                    if (!removeResult.Succeeded)
                    {
                        foreach (var error in removeResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("Profile", updatedUser);
                    }
                }

                var addResult = await _userManager.AddPasswordAsync(user, newPassword);
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Profile", updatedUser);
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
                return RedirectToAction("UserInfo", "Profile");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("Profile", updatedUser);
            }
        }

    }
}
