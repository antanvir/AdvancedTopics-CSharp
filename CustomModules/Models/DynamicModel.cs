namespace CustomModules.Models
{
    public class DynamicModel
    {
        public bool IsValidData { get; set; } = true;
        public Type ModelType { get; set; }
        public List<object> DataItems { get; set; }
        public object NewData { get; set; }
    }
}
