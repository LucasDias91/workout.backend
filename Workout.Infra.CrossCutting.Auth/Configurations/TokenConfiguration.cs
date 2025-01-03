

namespace Workout.Infra.CrossCutting.Security.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public int FinalExpiration { get; set; }
        public string Secret { get; set; }
    }
}
