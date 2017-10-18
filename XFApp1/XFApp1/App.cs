using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XFApp1.Views;

namespace XFApp1
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            Current.MainPage = new NavigationPage( new Page1());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
