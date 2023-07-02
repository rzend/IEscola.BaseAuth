
namespace IEscola.Auth
{
    public interface ISettings
    {
        Api[] Apis { get; set; }
    }


    public class Settings : ISettings
    {
        public Api[] Apis { get; set; }
    }

    public class Api
    {
        public string Secret { get; set; }
        public string ApiKey { get; set; }
        public string CredentialType { get; set; }
    }
}
