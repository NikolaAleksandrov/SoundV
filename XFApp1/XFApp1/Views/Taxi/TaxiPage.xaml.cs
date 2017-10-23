using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Speak();
            cancelSrc = null;
            flag = true;
        }

        private async void Speak()
        {
            await CrossTextToSpeech.Current.Speak("Double tap to call a taxi", null, null, 1.5f, null, cancelSrc.Token);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new TaxiCompanyPage());
            }
        }

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopAsync();
            }
        }

        private async void GoToBatteryLevelPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new BatteryLevelPage());
            }
        }
    }
}