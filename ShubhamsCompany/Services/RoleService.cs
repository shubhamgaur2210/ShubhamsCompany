using ShubhamsCompany.DAL;
using ShubhamsCompany.Models;

namespace ShubhamsCompany.Services
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        Role GetRoleByID(long roleID);
        int AddRole(Role role);
        int UpdateRole(Role role);
        int DeleteRole(long roleID);
    }
    public class RoleService : IRoleService
    {
        IRoleRepository roleRepository;
        public RoleService()
        {
            roleRepository = new RoleRepository();
        }
        public List<Role> GetAllRoles()
        {
            return roleRepository.GetAllRoles();
        }
        public Role GetRoleByID(long roleID)
        {
            return roleRepository.GetRoleByID(roleID);
        }
        public int AddRole(Role role)
        {
            return roleRepository.AddRole(role);
        }
        public int UpdateRole(Role role)
        {
            return roleRepository.UpdateRole(role);
        }
        public int DeleteRole(long roleID)
        {
            return roleRepository.DeleteRole(roleID);
        }
    }
}
