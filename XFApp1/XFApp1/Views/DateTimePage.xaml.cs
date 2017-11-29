using Plugin.TextToSpeech;
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
    public partial class DateTimePage : ContentPage
    {
        string currentDate = string.Empty;
        string requestedTime = string.Empty;

        bool flag;
        CancellationTokenSource cancelSrc;
        public DateTimePage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
            currentDate = DateTime.Now.Date.ToString();
        }
        

        protected override void OnAppearing()
        {
            currentDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
            TellTheDateLabel.Text = currentDate + ", " + DateTime.Today.DayOfWeek.ToString();
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Check the date", null, null, 1.5f, null, cancelSrc.Token));
            flag = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Double tap to check the date", null, null, 1.5f, null, cancelSrc.Token);
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe left or right");
                flag = true;
            }
        }

        private void TellTheDate(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now.Date.ToString("MM/dd/yyyy");

            TellTheDateLabel.Text = currentDate;
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("The date is" + currentDate + "." + DateTime.Today.DayOfWeek.ToString(), null, null, 1.0f, null, cancelSrc.Token);
        }
        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                await Navigation.PopAsync();
            }
        }

        private async void GoToHome(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new Home.Home());
            }
        }
    }
}
