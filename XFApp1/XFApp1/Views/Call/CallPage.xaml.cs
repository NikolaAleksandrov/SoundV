using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Views.Home;
using XFApp1.Views.Emergency;
using System.Threading;

namespace XFApp1.Views.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallPage : ContentPage
    {
        bool flag;
        CancellationTokenSource cancelSrc;
        public CallPage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Call page", null, null, 1.5f, null, cancelSrc.Token));
            flag = true;
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
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PushAsync(new CallTrustedPersonPage());
            }
        }
        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                await Navigation.PopAsync();
            }
        }

        private async void GoToEmergency(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PushAsync(new EmergencyPage());
            }
        }
    }
}