using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdvancedTopics.Models
{
    public class Employee
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The {0} value must be between {2} and {1}")]
        public string Name { get; set; }

        [DisplayName("Employee ID")]
        [RegularExpression(@"^[A-Z]{2,3}-[0-9]{4}$", ErrorMessage = "The Employee ID is not valid. Expected format: {{YY|ZZZ}}-{{XXXX}}")]
        public string EmpId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Tenure (yrs)")]
        [Range(1, 20, ErrorMessage = "The {0} value must be between {1} and {2}.")]
        public int Tenure  { get; set; }
    }
}
