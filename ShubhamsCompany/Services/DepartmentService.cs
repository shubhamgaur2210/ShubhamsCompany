using ShubhamsCompany.DAL;
using ShubhamsCompany.Models;

namespace ShubhamsCompany.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentByID(long departmentID);
        int AddDepartment(Department department);
        int UpdateDepartment(Department department);
        int DeleteDepartment(long departmentID);
    }
    public class DepartmentService : IDepartmentService
    {
        IDepartmentRepository departmentRepository;

        public DepartmentService()
        {
            departmentRepository = new DepartmentRepository();
        }

        public List<Department> GetAllDepartments()
        {
            return departmentRepository.GetAllDepartments();
        }

        public Department GetDepartmentByID(long departmentID)
        {
            return departmentRepository.GetDepartmentByID(departmentID);
        }

        public int AddDepartment(Department department)
        {
            return departmentRepository.AddDepartment(department);
        }

        public int UpdateDepartment(Department department)
        {
            return departmentRepository.UpdateDepartment(department);
        }

        public int DeleteDepartment(long departmentID)
        {
            return departmentRepository.DeleteDepartment(departmentID);
        }
    }
}
