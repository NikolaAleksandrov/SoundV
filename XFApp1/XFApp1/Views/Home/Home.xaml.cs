﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.ViewModels;

using Plugin.TextToSpeech;
using XFApp1.Views.Call;
using System.Threading;
using Plugin.Geolocator;

namespace XFApp1.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc;
        public Home()
        {
            InitializeComponent();
            Title = "Home";
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Разберете своето местоположение", null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PopAsync();
            }
        }

        //async void ReadPageText(object sender, EventArgs e)
        //{
        //    await CrossTextToSpeech.Current.Speak("Double tap to get location", null, null, 1.5f, null, cancelSrc.Token);
        //}

        async void GetLocation(object sender, EventArgs e)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 8);
            cancelSrc = new CancellationTokenSource();
            await CrossTextToSpeech.Current.Speak("Взимане на местоположение. Моля, изчакайте.", null, null, 1.0f, null, cancelSrc.Token);
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 30;
            var position = await locator.GetPositionAsync(timeout);
            var address = await locator.GetAddressesForPositionAsync(position);

            AddressLabel.Text = address.FirstOrDefault().FeatureName + ", " +
                address.FirstOrDefault().Thoroughfare + ", " +
                address.FirstOrDefault().AdminArea + ", " +
                address.FirstOrDefault().CountryName;
            if (position == null)
            {
                await CrossTextToSpeech.Current.Speak("Неуспешно взимание на местоположение.", null, null, 1.5f, null, cancelSrc.Token);
            }
            else
            {
                await CrossTextToSpeech.Current.Speak(AddressLabel.Text, null, null, 1.5f, null, cancelSrc.Token);
            }
        }

        async void GoToCallPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new CallPage());
            }
        }

        async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Dispose();
                await Navigation.PushAsync(new NavigateToHomePage());
            }
        }
    }
}
