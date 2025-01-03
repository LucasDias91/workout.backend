namespace Workout.Infra.CrossCutting.Security.DataModels
{
    public class AccessDataModel
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public AccessDataModel(
                          DateTime dataCriacao,
                          DateTime dataExpiracao,
                          string accessToken)
        {
            AccessToken = accessToken;
            Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss");
            Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss");
            AccessToken = accessToken;
            RefreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }


    }
}
