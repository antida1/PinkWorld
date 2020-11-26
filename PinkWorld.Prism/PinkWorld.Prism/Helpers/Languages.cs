﻿using PinkWorld.Common.Helpers;
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

        public static string Register => Resource.Register;

        public static string City => Resource.City;

        public static string CityError => Resource.CityError;

        public static string CityPlaceHolder => Resource.CityPlaceHolder;

        public static string Department => Resource.Department;

        public static string DepartmentError => Resource.DepartmentError;

        public static string DepartmentPlaceHolder => Resource.DepartmentPlaceHolder;

        public static string Country => Resource.Country;

        public static string CountryError => Resource.CountryError;

        public static string CountryPlaceHolder => Resource.CountryPlaceHolder;

        public static string Document => Resource.Document;

        public static string DocumentError => Resource.DocumentError;

        public static string DocumentPlaceHolder => Resource.DocumentPlaceHolder;

        public static string FirstName => Resource.FirstName;

        public static string FirstNameError => Resource.FirstNameError;

        public static string FirstNamePlaceHolder => Resource.FirstNamePlaceHolder;

        public static string SecondName => Resource.SecondName;

        public static string SecondNameError => Resource.SecondNameError;

        public static string SecondNamePlaceHolder => Resource.SecondNamePlaceHolder;

        public static string LastName => Resource.LastName;

        public static string LastNameError => Resource.LastNameError;

        public static string LastNamePlaceHolder => Resource.LastNamePlaceHolder;

        public static string Address => Resource.Address;

        public static string AddressError => Resource.AddressError;

        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;

        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string Error001 => Resource.Error001;

        public static string Error002 => Resource.Error002;

        public static string Error003 => Resource.Error003;

        public static string Ok => Resource.Ok;

        public static string RegisterMessge => Resource.RegisterMessge;
        
        public static string CreateAccount => Resource.CreateAccount;

        public static string PictureSource => Resource.PictureSource;

        public static string Cancel => Resource.Cancel;

        public static string FromCamera => Resource.FromCamera;

        public static string FromGallery => Resource.FromGallery;

        public static string NoCameraSupported => Resource.NoCameraSupported;

        public static string NoGallerySupported => Resource.NoGallerySupported;

    }

}
