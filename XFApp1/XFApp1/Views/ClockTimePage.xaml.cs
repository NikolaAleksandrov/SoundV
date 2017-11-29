using Plugin.TextToSpeech;
using SoundV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockTimePage : ContentPage
    {
        string currentTime = string.Empty;
        string requestedTime = string.Empty;

        bool flag;
        CancellationTokenSource cancelSrc;
        public ClockTimePage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
            currentTime = DateTime.Now.ToLocalTime().ToString();
        }

        protected override void OnAppearing()
        { 
            currentTime = DateTime.Now.ToLocalTime().ToString();
            TellTheTimeLabel.Text = currentTime;
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Check your time", null, null, 1.5f, null, cancelSrc.Token));
            flag = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Double tap to check the time", null, null, 1.5f, null, cancelSrc.Token);
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Swipe right to check your location");
                flag = true;
            }
        }

        private void TellTheTime(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now.Date.ToString();

            TellTheTimeLabel.Text = currentDate;
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("The date is" + currentDate, null, null, 1.0f, null, cancelSrc.Token);
        }

        private async void GoToHome(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PushAsync(new Home.Home());
            }
        }
    }
}

