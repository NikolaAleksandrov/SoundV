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
        private string currentDate = string.Empty;
        private string requestedTime = string.Empty;
        private string currentDayOfWeek = string.Empty;
        private bool flag;

        CancellationTokenSource cancelSrc;
        public DateTimePage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);

            currentDate = DateTime.Now.Date.ToString();
            currentDayOfWeek = DateTime.Now.Date.DayOfWeek.ToString();
        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            currentDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
            currentDayOfWeek = DateTime.Now.Date.DayOfWeek.ToString(); 
            switch (currentDayOfWeek)
            {
                case "Monday":
                    currentDayOfWeek = "Понеделник";
                    break;
                case "Tuesday":
                    currentDayOfWeek = "Вторник";
                    break;
                case "Wednesday":
                    currentDayOfWeek = "Сряда";
                    break;
                case "Thursday":
                    currentDayOfWeek = "Четвъртък";
                    break;
                case "Friday":
                    currentDayOfWeek = "Петък";
                    break;
                case "Saturday":
                    currentDayOfWeek = "Събота";
                    break;
                case "Sunday":
                    currentDayOfWeek = "Неделя";
                    break;
                default: currentDayOfWeek = "";
                    break;
            }

            TellTheDateLabel.Text = currentDate + ",  " + currentDayOfWeek;

            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Провери датата", null, null, null, null, cancelSrc.Token));
            flag = true;
        }


        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await CrossTextToSpeech.Current.Speak("Няма страници в тази посока.");
                flag = true;
            }
        }

        private void TellTheDate(object sender, EventArgs e)
        {
            var currentDate = DateTime.Now.Date.ToString("MM/dd/yyyy");

            TellTheDateLabel.Text = currentDate + ",  " + currentDayOfWeek;
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Датата е" + currentDate + ". " + currentDayOfWeek, null, null, 1.0f, null, cancelSrc.Token);
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
