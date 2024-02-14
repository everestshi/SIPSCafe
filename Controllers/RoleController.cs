using Microsoft.AspNetCore.Mvc;
using Sips.Data;
using Sips.Repositories;
using Sips.SipsModels.ViewModels;

namespace Sips.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RoleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public ActionResult Index(string message = "")
        {
            RoleRepo roleRepo = new RoleRepo(_db);
            ViewBag.Message = message;
            return View(roleRepo.GetAllRoles());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_db);
                bool isSuccess =
                    roleRepo.CreateRole(roleVM.RoleName);

                if (isSuccess)
                {
                    //return RedirectToAction(nameof(Index));
                    string message = "Role created successfully.";
                    return RedirectToAction("Index", "Role",
                        new
                        {
                            message = message
                        });
                }
                else
                {
                    ModelState
                    .AddModelError("", "Role creation failed.");
                    ModelState
                    .AddModelError("", "The role may already" +
                                       " exist.");
                }
            }

            return View(roleVM);
        }
        //public ActionResult Delete(string roleName)
        //{
        //    RoleRepo roleRepo = new RoleRepo(_db);
        //    //var role = roleRepo.GetRole(roleName); 

        //    // Check if the role is assigned to any user
        //    var isRoleAssigned = roleRepo.IsRoleAssigned(roleName);

        //    if (isRoleAssigned)
        //    {
        //        string message = "Role cannot be deleted because it is assigned to one or more users.";

        //        return RedirectToAction("Index", "Role", new { message = message }); // Redirect to the Index page
        //    }

        //    return View(new RoleVM { RoleName = roleName });
        //}

        //[HttpPost]
        //public ActionResult Delete(RoleVM role)
        //{
        //    RoleRepo roleRepo = new RoleRepo(_db);

        //    bool isSuccess = roleRepo.DeleteRole(role.RoleName);
        //    string message = "";

        //    if (isSuccess)
        //    {
        //        message = "Role deleted successfully.";
        //    }
        //    else
        //    {
        //        message = "Role deletion failed.";
        //    }

        //    return RedirectToAction("Index", "Role", new { message = message }); // Redirect to the Index page
        //}

    }
}

