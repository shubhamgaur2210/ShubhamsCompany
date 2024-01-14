using ShubhamsCompany.DAL;
using ShubhamsCompany.Models;
using ShubhamsCompany.Models.ViewModels;

namespace ShubhamsCompany.Services
{
    public interface IUserService
    {
        List<UserViewModel> GetAllUsers();
        UserViewModel GetUserByID(long userID);
        int AddUser(User user);
        int UpdateUser(User user);
        int DeleteUser(long userID);
    }
    public class UserService: IUserService
    {
        IUserRepository userRepository;
        IRoleRepository roleRepository;
        IDepartmentRepository departmentRepository;

        public UserService()
        {
            userRepository = new UserRepository();
            roleRepository = new RoleRepository();
            departmentRepository = new DepartmentRepository();
        }

        public List<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            List<User> us = userRepository.GetAllUsers();
            foreach (User user in us)
            {
                Role role = roleRepository.GetRoleByID(user.RoleID);
                Department department = departmentRepository.GetDepartmentByID(user.DepartmentID);
                users.Add(new UserViewModel
                {
                    User = user,
                    Role = role,
                    Department = department
                });
            }

            return users;
        }

        public UserViewModel GetUserByID(long userID)
        {
            UserViewModel user = new UserViewModel();
            User us = userRepository.GetUserByID(userID);
            if (us != null)
            {
                Role role = roleRepository.GetRoleByID(us.RoleID);
                Department department = departmentRepository.GetDepartmentByID(us.DepartmentID);

                user.User = us;
                user.Role = role;
                user.Department = department;
            }

            return user;
        }

        public int AddUser(User user)
        {
            return userRepository.AddUser(user);
        }

        public int UpdateUser(User user)
        {
            return userRepository.UpdateUser(user);
        }

        public int DeleteUser(long userID)
        {
            return userRepository.DeleteUser(userID);
        }
    }
}
