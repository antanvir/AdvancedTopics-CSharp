using CustomModules.Models;

namespace CustomModules.Modules
{
    public class Publisher
    {
        public event EventHandler<CustomEventArgs> HandleCustomEvent;

        public void RaiseCustomEvent(Athlete athlete)
        {
            HandleEvent(new CustomEventArgs(athlete));
        }

        public void HandleEvent(CustomEventArgs args)
        {
            if (HandleCustomEvent != null)
            {
                HandleCustomEvent(this, args);
            }
        }
    }
}
