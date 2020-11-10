using PinkWorld.Common.Models;
using Prism.Navigation;

namespace PinkWorld.Prism.ViewModels
{
    public class QuizItemViewModel : QuizResponse
    {
        private readonly INavigationService _navigationService;

        public QuizItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
