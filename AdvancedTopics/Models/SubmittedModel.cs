namespace AdvancedTopics.Models
{
    public class SubmittedModel
    {
        public bool IsValidData { get; set; }
        public string ModelType { get; set; }
        public List<Employee> DataItems { get; set; }
        public Employee NewData { get; set; }
    }
}
