
using courses_registration.Resources;
using Microsoft.Extensions.Localization;

namespace courses_registration.Controllers
{
  
    public class LocalizationController
    {
        private readonly IStringLocalizer<Messages> _localizer;

        public LocalizationController(IStringLocalizer<Messages> localizer)
        {
            _localizer = localizer;
        }

        public string GetLocalized(string key)
        {
            return _localizer[key];
        }
    }
}
