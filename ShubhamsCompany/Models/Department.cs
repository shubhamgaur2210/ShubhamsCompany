using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShubhamsCompany.Models
{
    public class Department
    {
        [Key]
        public long DepartmentID { get; set; }

        [DisplayName("Department Name")]
        [Required(ErrorMessage = "DepartmentName is required.")]
        public string DepartmentName { get; set; }
    }
}
