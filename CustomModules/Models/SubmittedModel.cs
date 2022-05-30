using CustomModules.Filters;

namespace CustomModules.Models
{
    [DuplicateCheck(ErrorMessage = "The Employee ID/Email already exists. Please provide a unique one.")]
    public class SubmittedModel
    {
        public bool IsValidData { get; set; }
        public string ModelType { get; set; }
        public List<Employee> DataItems { get; set; }
        public Employee NewData { get; set; }
    }
}
