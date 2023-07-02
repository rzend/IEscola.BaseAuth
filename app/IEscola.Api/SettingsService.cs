using Microsoft.Extensions.Options;

namespace IEscola.Api
{
    public class SettingsService : ISettingsService
    {
        private readonly Settings _settings;
        public SettingsService(IOptions<Settings> settings)
        {
            _settings = settings?.Value;
        }

        public Settings GetSettings()
        {
            return _settings;
        }
    }

    public interface ISettingsService
    {
        Settings GetSettings();
    }
}
