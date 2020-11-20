using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Model = PinkWorld.Prism.Models.Catalog.Travel;

namespace PinkWorld.Prism.ViewModels.Catalog
{
    /// <summary>
    /// ViewModel for navigation travel page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class NavigationTravelPageViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Model> travelPlaces;

        private ObservableCollection<Model> topDestinations;

        private ObservableCollection<Model> bestPlaces;

        private Command addFavouriteCommand;

        private DelegateCommand _link;

        private Command selectionCommand;

        private int selectedIndex;
        private readonly INavigationService _navigationService;

        #endregion



        /// <summary>
        /// Initializes a new instance for the <see cref="NavigationTravelPageViewModel" /> class.
        /// </summary>
        public NavigationTravelPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.TravelPlaces = new ObservableCollection<Model>
            {
                new Model
                {
                    ImagePath = App.BaseImageUrl + "CMain.png",
                    Place = "We can all fight it",
                    Details = "This disease affects 1 in 8 women according to a Globocan study."
                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "C1.png",
                    Place = "First Step"

                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "C2.png",
                    Place = "Second Step",
                   
                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "C3.png",
                    Place = "Third Step",

                },
                 new Model
                {
                    ImagePath = App.BaseImageUrl + "C4.png",
                    Place = "Four Step"
                  
                }
            };

            this.TopDestinations = new ObservableCollection<Model>
            {
                new Model
                {
                    ImagePath = App.BaseImageUrl + "P1.jpeg",
                    Place = "Take care, Examine",
                    Details = "The test collects cells with a fine needle, analyzes the results and generates a diagnosis."
                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "P2.png",
                    Place = "Together we are stronger",
                    Details = "Breast cancer affects both men and women."
                },
                new Model
                {
                    ImagePath =  App.BaseImageUrl + "P3.jpeg",
                    Place ="Breast cancer and the pandemic",
                    Details = "The pandemic one more obstacle"
                },
                new Model
                {
                    ImagePath = "",
                    Place = "Madrid, Spain",
                    Details = "The capital of Spain tends to be overlooked in favor of the colorful Barcelona, but this city has a glamor of its own."
                },
            };

            this.BestPlaces = new ObservableCollection<Model>
            {
                new Model
                {
                    ImagePath = App.BaseImageUrl + "Afiche-01-483x690.jpg",
                   
                    
                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "C9.jpeg",
                    
                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "C10.jpg",
                    
                },
                new Model
                {
                    ImagePath = App.BaseImageUrl + "C11.jpg",
                    
                },
            };

            this.TravelPlacesCommand = new Command(this.TravelPlacesClicked);
            this.TopDestinationsCommand = new Command(this.TopDestinationsClicked);
            this.BestPlacesCommand = new Command(this.BestPlacesClicked);
            this.ItemSelectedCommand = new Command(this.ItemSelected);
            _navigationService = navigationService;
        }

       

        #region Public Properties

        /// <summary>
        /// Gets or sets the property that has been bound with the rotator view, which displays the travel places and details.
        /// </summary>
        public ObservableCollection<Model> TravelPlaces
        {
            get
            {
                return this.travelPlaces;
            }

            set
            {
                if (this.travelPlaces == value)
                {
                    return;
                }

                this.travelPlaces = value;
               
            }
        }

        /// <summary>
        /// Gets or sets the property which displays top destinations, details and price.
        /// </summary>
        public ObservableCollection<Model> TopDestinations
        {
            get
            {
                return this.topDestinations;
            }

            set
            {
                if (this.topDestinations == value)
                {
                    return;
                }

                this.topDestinations = value;
               
            }
        }

        /// <summary>
        /// Gets or sets the property which displays the names and images of best places.
        /// </summary>
        public ObservableCollection<Model> BestPlaces
        {
            get
            {
                return this.bestPlaces;
            }

            set
            {
                if (this.bestPlaces == value)
                {
                    return;
                }

                this.bestPlaces = value;
      
            }
        }

        /// <summary>
        /// Gets or sets the selected index of the rotator.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex == value)
                {
                    return;
                }
                selectedIndex = value;
        
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will executed when the travel places item is clicked.
        /// </summary>
        public Command TravelPlacesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will executed when the top destinations item is clicked.
        /// </summary>
        public Command TopDestinationsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will executed when the best places item is clicked.
        /// </summary>
        public Command BestPlacesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }


        public DelegateCommand LinkCommand => _link ?? (_link = new DelegateCommand(ItemSelected));

        /// <summary>
        /// Gets or sets the command that will be executed when the favourite button is clicked.
        /// </summary>
        public Command AddFavouriteCommand
        {
            get
            {
                return this.addFavouriteCommand ?? (this.addFavouriteCommand = new Command(this.AddFavouriteClicked));
            }
        }

        // <summary>
        /// Gets or sets the command that will be executed when carousel view item is swiped.
        /// </summary>   
        public Command SelectionCommand
        {
            get
            {
                return this.selectionCommand ?? (this.selectionCommand = new Command(this.SelectionClicked));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the the travel places item is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void TravelPlacesClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the the top destinations item is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void TopDestinationsClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the the best places item is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void BestPlacesClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ItemSelected()
        {
            var uri = new Uri("https://www.bbc.com/mundo/noticias-54571220");
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        /// <summary>
        /// Invoked when the favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddFavouriteClicked(object obj)
        {
            if (obj is Model travel)
                travel.IsFavourite = !travel.IsFavourite;
        }

        /// <summary>
        /// Invoked when carousel view item is swiped.
        /// </summary>
        /// <param name="obj">The rotator item</param>
        private void SelectionClicked(object obj)
        {
            this.SelectedIndex = this.TravelPlaces.IndexOf(obj);
        }
        #endregion
    }
}