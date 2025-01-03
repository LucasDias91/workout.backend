using Workout.Infra.CrossCutting.Security.DataModels;

namespace Workout.Infra.CrossCutting.Security.Services.Interfaces
{
    public interface ITokenService
    {
        AccessDataModel CreateToken(string userName, int userId, string sessionKey);
        void Logout(string refreshToken);
        bool IsAuthorized(string refreshToken);
    }
}
