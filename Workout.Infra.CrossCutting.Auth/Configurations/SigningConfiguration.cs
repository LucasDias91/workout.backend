using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Workout.Infra.CrossCutting.Security.Configurations
{
    public class SigningConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguration(string key)
        {
            SigningCredentials = new SigningCredentials(GetBytes(key), SecurityAlgorithms.RsaSha256Signature);
        }

        private SymmetricSecurityKey GetBytes(string key) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));


    }
}
