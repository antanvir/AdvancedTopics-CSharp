namespace CustomModules.Models
{
    public class Athlete
    {
        public Athlete()
        {
            EligibleForSports = new List<string>();
        }
        public string? Name { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public List<string> EligibleForSports { get; set; }
    }
}
