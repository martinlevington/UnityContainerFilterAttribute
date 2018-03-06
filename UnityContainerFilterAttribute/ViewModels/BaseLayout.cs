using System.Globalization;

namespace UnityContainerFilterAttribute.ViewModels
{
    public class BaseLayout
    {
        public string TwoLetterIsoLanguageName => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        public string UiCulture => CultureInfo.CurrentUICulture.ToString();

        public string BaseName { get; set; }
        public long ExecutionTime { get; set; }
    }
}