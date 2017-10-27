using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Interface;
using XFApp1.Views.Battery;
using XFApp1.Views.Settings;

namespace XFApp1.Views.Taxi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxiPage : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc;

        public TaxiPage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Call a Taxi Menu", null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Call a Taxi Menu", null, null, 1.5f, null, cancelSrc.Token);
        }

        private async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new TaxiCompanyPage());
            }
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

        private async void GoToBatteryLevelPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new BatteryLevelPage());
            }
        }
    }
}