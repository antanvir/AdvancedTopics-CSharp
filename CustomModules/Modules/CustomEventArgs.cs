using CustomModules.Models;

namespace CustomModules.Modules
{
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(Athlete newAthlete)
        {
            NewAthlete = newAthlete;
        }

        public Athlete NewAthlete { get; set; }
    }
}
