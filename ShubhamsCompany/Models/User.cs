using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using ShubhamsCompany.DAL;

namespace ShubhamsCompany.Models
{
    public class User: IValidatableObject
    {
        [Key]
        public long UserID { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LName { get; set; }

        [NotMapped]
        [DisplayName("Full Name")]
        public string FullName {
            get { return FName + " " + LName; }
        }

        [DisplayName("Date Of Joining")]
        [Required(ErrorMessage = "DOJ is required.")]
        public DateTime DOJ { get; set; }

        [DisplayName("Last Login")]
        public DateTime? LastLogin { get; set; }

        [Range(0, 9.99, ErrorMessage = "Seniority must be between 0 and 9.99.")]
        public decimal Seniority { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public long RoleID { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public long DepartmentID { get; set; }

        [Required(ErrorMessage = "EmpCode is required.")]
        [RegularExpression("[A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9]", ErrorMessage = "Invalid EmpCode format. Must be of format XXX999")]
        public string EmpCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsFieldsUnique(UserName, EmpCode))
            {
                yield return new ValidationResult("UserName and EmpCode must be unique.", new[] { nameof(UserName), nameof(EmpCode) });
            }
        }
        // Method to check if UserName and EmpCode are unique
        private bool IsFieldsUnique(string userName, string empCode)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.IsFieldsUnique(userName, empCode);
        }
    }
}
