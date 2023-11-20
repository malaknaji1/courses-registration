using courses_registration.Resources;
using Microsoft.Extensions.Localization;

namespace courses_registration.Helpers
{
    public class Localizer
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public Localizer(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;
        }

        public string GetLocalized(string key , string? optional="")
        {
            return _localizer[key,optional];
        }
    }
}
