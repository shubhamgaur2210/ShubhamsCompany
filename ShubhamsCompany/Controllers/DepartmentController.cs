using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShubhamsCompany.Models;
using ShubhamsCompany.Services;

namespace ShubhamsCompany.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService departmentService;

        public DepartmentController()
        {
            departmentService = new DepartmentService();
        }

        // GET: DepartmentController/Index
        public ActionResult Index()
        {
            List<Department> departments = departmentService.GetAllDepartments();
            return View(departments);
        }

        // GET: DepartmentController/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: DepartmentController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int row = departmentService.AddDepartment(department);
                    if (row > 0)
                    {
                        TempData["SuccessMessage"] = "Department Added Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["FailureMessage"] = "Failure Occured.";
                        return View(department);
                    }
                }
                else
                {
                    TempData["InvalidMessage"] = "Invalid input.";
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(department);
            }
        }

        // GET: DepartmentController/Update/5
        public ActionResult Update(int id)
        {
            Department department = departmentService.GetDepartmentByID(id);
            if (department == null)
            {
                TempData["InvalidMessage"] = "No Department found with the DepartmentID";
                return RedirectToAction("Index");
            }
            else
            {
                return View(department);
            }
        }

        // POST: DepartmentController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int row = departmentService.UpdateDepartment(department);
                    if (row > 0)
                    {
                        TempData["SuccessMessage"] = "Department Updated Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["FailureMessage"] = "Failure Occured.";
                        return View(department);
                    }
                }
                else
                {
                    TempData["InvalidMessage"] = "Invalid input.";
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(department);
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            Department department = departmentService.GetDepartmentByID(id);
            if (department == null)
            {
                TempData["InvalidMessage"] = "No Department found with the DepartmentID";
                return RedirectToAction("Index");
            }
            else
            {
                return View(department);
            }
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Department department)
        {
            try
            {
                int row = departmentService.DeleteDepartment(department.DepartmentID);
                if (row > 0)
                {
                    TempData["SuccessMessage"] = "Department Deleted Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["FailureMessage"] = "Failure Occured.";
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(department);
            }
        }
    }
}
