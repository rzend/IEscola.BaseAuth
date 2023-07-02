
namespace IEscola.Api
{
    public class Settings : ISettings
    {
        public string Secret { get; set; }
        public string MyApiKey { get; set; }
    }

    public interface ISettings
    {
        string Secret { get; set; }
        string MyApiKey { get; set; }
    }
}
