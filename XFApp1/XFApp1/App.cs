using SoundV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XFApp1.ViewModels;
using XFApp1.Views;
using XFApp1.Views.Home;

namespace XFApp1
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            Current.MainPage = new NavigationPage(new Home());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Application.Current.Properties["HomePlace"] = "";
            Application.Current.Properties["SavedPlace1"] = "";
            Application.Current.Properties["SavedPlace2"] = "";
          

            Application.Current.Properties["TrustedPersonName"] = "";
            Application.Current.Properties["TrustedPersonPhoneNumber"] = "";

            
            Application.Current.Properties["Company1Name"] = "";
            Application.Current.Properties["Company1PhoneNumer"] = "";
            Application.Current.Properties["Company2Name"] = "";
            Application.Current.Properties["Company2PhoneNumer"] = "";
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
