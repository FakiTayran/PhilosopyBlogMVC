using BlogMVC.Areas.ViewModels;
using BlogMVC.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminAccountController : BaseController
    {
        // GET: Admin/AdminAccount
        public ActionResult Index(string result)
        {
            ViewBag.Active = "Accounts";
            ViewBag.result = result;
            var data = db.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.NickName,
                Email = x.Email,
                Roles = x.Roles.Join(db.Roles, userRole => userRole.RoleId, role => role.Id, (t1, t2) => t2.Name).ToList()
            }).ToList();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GiveAdminRole(string userId, bool IsAdmin)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (!string.IsNullOrEmpty(userId))
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var adminRole = roleManager.FindByName("admin");
                var adminCount = adminRole.Users.Count();
                if (IsAdmin == true)
                {
                    await userManager.AddToRoleAsync(userId, "admin");
                    return RedirectToAction("Index", new { result = "rolAdded" });
                }
                else
                {
                    if (adminCount == 1 && !IsAdmin)
                    {
                        return RedirectToAction("Index", new { result = "OneAdmin" });
                    }

                    await userManager.RemoveFromRolesAsync(userId, "admin");
                    return RedirectToAction("Index", new { result = "rolRemoved" });
                }
            }
            return RedirectToAction("Index", new { result = "errorRolChanging" });
        }

        public async Task<ActionResult> DeleteUser(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", new { result = "userNotFind" });
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index", new { result = "userNotFind" });
            }
            if (await userManager.IsInRoleAsync(user.Id, "admin"))
            {
                return RedirectToAction("Index", new { result = "adminDeleteError" });
            }

            var articleUser = db.Comments.Where(x => x.ApplicationUserId == id);
            if (articleUser != null)
            {
                db.Comments.RemoveRange(articleUser);
                db.SaveChanges();
            }
    
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index", new { result = "userDeleted" });
        }

        public async Task<ActionResult> PasswordChange(string UserId, string password)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            string errorMessage = null;
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password))
            {
                errorMessage = "You have to input new password.";
            }
            if (string.IsNullOrEmpty(errorMessage))
            {
                await userManager.RemovePasswordAsync(UserId);
                IdentityResult result = await userManager.AddPasswordAsync(UserId, password);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                else
                {
                    errorMessage = string.Join(" ,", result.Errors);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { error = errorMessage });
        }

    }
}