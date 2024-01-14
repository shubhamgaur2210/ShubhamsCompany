using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShubhamsCompany.Models
{
    public class Role
    {
        [Key]
        public long RoleID { get; set; }

        [DisplayName("Role Name")]
        [Required(ErrorMessage = "RoleName is required.")]
        public string RoleName { get; set; }
    }
}
