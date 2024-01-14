using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShubhamsCompany.Services;
using ShubhamsCompany.Models;

namespace ShubhamsCompany.Controllers
{
    public class RoleController : Controller
    {
        private IRoleService roleService;

        public RoleController()
        {
            roleService = new RoleService();
        }

        // GET: RoleController/Index
        public ActionResult Index()
        {
            List<Role> roles = roleService.GetAllRoles();
            return View(roles);
        }

        // GET: RoleController/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: RoleController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Role role)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int row = roleService.AddRole(role);
                    if(row > 0)
                    {
                        TempData["SuccessMessage"] = "Role Added Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["FailureMessage"] = "Failure Occured.";
                        return View(role);
                    }
                }
                else
                {
                    TempData["InvalidMessage"] = "Invalid input.";
                    return View(role);
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(role);
            }
        }

        // GET: RoleController/Update/5
        public ActionResult Update(int id)
        {
            Role role = roleService.GetRoleByID(id);
            if(role == null)
            {
                TempData["InvalidMessage"] = "No Role found with the RoleID";
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }

        // POST: RoleController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int row = roleService.UpdateRole(role);
                    if (row > 0)
                    {
                        TempData["SuccessMessage"] = "Role Updated Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["FailureMessage"] = "Failure Occured.";
                        return View(role);
                    }
                }
                else
                {
                    TempData["InvalidMessage"] = "Invalid input.";
                    return View(role);
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(role);
            }
        }

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            Role role = roleService.GetRoleByID(id);
            if (role == null)
            {
                TempData["InvalidMessage"] = "No Role found with the RoleID";
                return RedirectToAction("Index");
            }
            else
            {
                return View(role);
            }
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Role role)
        {
            try
            {
                int row = roleService.DeleteRole(role.RoleID);
                if (row > 0)
                {
                    TempData["SuccessMessage"] = "Role Deleted Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["FailureMessage"] = "Failure Occured.";
                    return View(role);
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(role);
            }
        }
    }
}
