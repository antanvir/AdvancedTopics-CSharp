using CustomModules.Models;
using CustomModules.Modules;
using System.Reflection;

namespace AdvancedTopics.Helpers
{
    public class SportsSelectionHelper
    {
        private readonly IConfiguration _configuration;
        private readonly Publisher _publisher;

        public delegate void EligibilityChecker(Athlete athlete);

        public SportsSelectionHelper(IConfiguration configuration, Publisher pub)
        {
            _configuration = configuration;
            _publisher = pub;

            pub.HandleCustomEvent += IsFitForIndoorGames;
            pub.HandleCustomEvent += IsFitForCricket;
            pub.HandleCustomEvent += IsFitForFootball;
            pub.HandleCustomEvent += IsFitForBasketball;
            pub.HandleCustomEvent += (sender, args) => {
                args.NewAthlete.EligibleForSports.Add("archery");
            };
        }

        public async Task<Athlete> ProcessAthleteRegistration(Athlete athlete)
        {            
            string path = _configuration["AthleteRepository"];
            using (StreamWriter file = File.AppendText(path + "Athletes-Data.txt"))
            {
                
                foreach (PropertyInfo prop in athlete.GetType().GetProperties())
                {
                    string line = $"{prop.Name}: {prop.GetValue(athlete, null)?.ToString()}";
                    await file.WriteLineAsync(line);
                }
            }

            _publisher.RaiseCustomEvent(athlete);

            return athlete;
        }
        public static void IsFitForIndoorGames(object sender, CustomEventArgs args)
        {
            if (args.NewAthlete.EligibleForSports == null) args.NewAthlete.EligibleForSports = new List<string>();
            args.NewAthlete.EligibleForSports.Add("indoor-games");
        }

        public void IsFitForCricket(object sender, CustomEventArgs args)
        {
            if (!(args.NewAthlete.Height <= 6.0 && args.NewAthlete.Weight > 120.0))
            {
                args.NewAthlete.EligibleForSports.Add("cricket");
            }
        }

        public void IsFitForFootball(object sender, CustomEventArgs args)
        {
            if (!(args.NewAthlete.Height <= 6.0 && args.NewAthlete.Weight > 100.0) && 
                (args.NewAthlete.Height > 5.6 && args.NewAthlete.Weight <= 120.0))
            {
                args.NewAthlete.EligibleForSports.Add("football");
            }
        }

        public void IsFitForBasketball(object sender, CustomEventArgs args)
        {
            if (args.NewAthlete.Height > 6.0 && args.NewAthlete.Weight < 120.0)
            {
                args.NewAthlete.EligibleForSports.Add("basketball");
            }
        }

    }
}
