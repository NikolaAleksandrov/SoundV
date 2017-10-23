using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Views.Taxi;

namespace XFApp1.Views.Emergency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencyPage : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc;
        public EmergencyPage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Call an emergency number", null, null, 1.5f,null, cancelSrc.Token);
            flag = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            cancelSrc.Cancel();
            cancelSrc.Dispose();
        }

        private async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new EmergencySubLevel());
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

        private async void GoToTaxiPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new TaxiPage());
            }
        }

    }
}