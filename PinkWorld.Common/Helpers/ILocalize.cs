using System.Globalization;

namespace PinkWorld.Common.Helpers
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);

    }
}
