using PinkWorld.Common.Helpers;
using PinkWorld.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace PinkWorld.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Loading => Resource.Loading;

        public static string Quizzes => Resource.Quizzes;

        public static string Login => Resource.Login;

        public static string ShowQuizzesHistory => Resource.ShowQuizzesHistory;

        public static string RegisterSelfExamination => Resource.RegisterSelfExamination;

        public static string ModifyUser => Resource.ModifyUser;

        public static string ViewStadistics => Resource.ViewStadistics;

        public static string SeeMaps => Resource.SeeMaps;
        public static string Main => Resource.Main;
        public static string Menu => Resource.Menu;

    }

}
