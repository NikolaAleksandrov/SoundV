using Plugin.TextToSpeech;
using Plugin.Vibrate;
using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockTimePage : ContentPage
    {
        string currentTime;

        bool flag;
        CancellationTokenSource cancelSrc;
        public ClockTimePage()
        {
            InitializeComponent();
            flag = true;
            var tapGestureRecognizer = new TapGestureRecognizer();

            WalkerPicture.GestureRecognizers.Add(tapGestureRecognizer);
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
            currentTime = DateTime.Now.ToString("HH:mm");
        }

        protected override void OnAppearing()
        {
            currentTime = DateTime.Now.ToString("HH:mm");
            TellTheTimeLabel.Text = currentTime;
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => {  await CrossTextToSpeech.Current.Speak("Проверете часа", null, null,null, null, cancelSrc.Token); });
            flag = true;
        }

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                flag = false;
                await Navigation.PopAsync();
            }
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Dispose();
                Task.Run(async () =>
                {
                    await CrossTextToSpeech.Current.Speak("Проверете часа", null, null, null, null, cancelSrc.Token);
                });
                flag = true;
            }
        }

        private void TellTheTime(object sender, EventArgs e)
        {
            var currentTime = DateTime.Now.ToString("HH:mm");

            TellTheTimeLabel.Text = currentTime;
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak(currentTime, null, null, null, null, cancelSrc.Token);
        }

        private async void GoToDate(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new DateTimePage());
            }
        }
    }
}

