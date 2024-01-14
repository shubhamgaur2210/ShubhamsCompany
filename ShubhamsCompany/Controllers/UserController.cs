using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShubhamsCompany.Models;
using ShubhamsCompany.Models.ViewModels;
using ShubhamsCompany.Services;
using System.Data;

namespace ShubhamsCompany.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;
        private IRoleService roleService;
        private IDepartmentService departmentService;

        public UserController()
        { 
            userService = new UserService();
            roleService = new RoleService();
            departmentService = new DepartmentService();
        }

        // GET: UserController
        public ActionResult Index()
        {
            List<UserViewModel> users = userService.GetAllUsers();
            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            UserViewModel user = userService.GetUserByID(id);
            if (user == null)
            {
                TempData["InvalidMessage"] = "No User found with the UserID";
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        // GET: UserController/Add
        public ActionResult Add()
        {
            ViewBag.Roles = roleService.GetAllRoles();
            ViewBag.Departments = departmentService.GetAllDepartments();
            return View();
        }

        // POST: UserController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User user)
        {
            ViewBag.Roles = roleService.GetAllRoles();
            ViewBag.Departments = departmentService.GetAllDepartments();
            try
            {
                if(ModelState.IsValid)
                {
                    user.LastLogin = DateTime.Now;  // as of now the Last login will be the last time user was edited or created(by default)
                    int row = userService.AddUser(user);
                    if (row > 0)
                    {
                        TempData["SuccessMessage"] = "User Added Successfully.";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["FailureMessage"] = "Failure Occured.";
                        return View(user);
                    }
                }
                else
                {
                    TempData["InvalidMessage"] = "Invalid input.";
                    return View(user);
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(user);
            }
        }

        // GET: UserController/Update/5
        public ActionResult Update(int id)
        {
            UserViewModel user = userService.GetUserByID(id);
            if(user == null)
            {
                TempData["InvalidMessage"] = "No User found with the UserID";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Roles = roleService.GetAllRoles();
                ViewBag.Departments = departmentService.GetAllDepartments();
                return View(user.User);
            }
        }

        // POST: UserController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(User user)
        {
            ViewBag.Roles = roleService.GetAllRoles();
            ViewBag.Departments = departmentService.GetAllDepartments();
            try
            {
                if (ModelState.IsValid)
                {
                    user.LastLogin = DateTime.Now;  // as of now the Last login will be the last time user was edited or created(by default)
                    int row = userService.UpdateUser(user);
                    if (row > 0)
                    {
                        TempData["SuccessMessage"] = "User Updated Successfully.";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        TempData["FailureMessage"] = "Failure Occured.";
                        return View(user);
                    }
                }
                else
                {
                    TempData["InvalidMessage"] = "Invalid input.";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(user);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            UserViewModel userVM = userService.GetUserByID(id);
            if (userVM == null)
            {
                TempData["InvalidMessage"] = "No User found with the RoleID";
                return RedirectToAction("Index");
            }
            else
            {
                return View(userVM);
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeletePost")]
        public ActionResult DeletePost(int id)
        {
            try
            {
                int row = userService.DeleteUser(id);
                if (row > 0)
                {
                    TempData["SuccessMessage"] = "User Deleted Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["FailureMessage"] = "Failure Occured.";
                    return View("Delete", userService.GetUserByID(id));
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View("Delete", userService.GetUserByID(id));
            }
        }
    }
}
