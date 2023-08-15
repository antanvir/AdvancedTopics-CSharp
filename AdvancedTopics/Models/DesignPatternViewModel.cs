using AdvancedTopics.Helpers.Command;

namespace AdvancedTopics.Models
{
    public class DesignPatternViewModel
    {
        ShopCart ShopCart { get; set; }
        Book Book { get; set; }
        public string Action { get; set; }
    }
}
