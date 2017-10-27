using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsTaxi : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public SettingsTaxi()
        {
            InitializeComponent();
            flag = true;
            NavigationPage.SetHasNavigationBar(this, false);
        }
      


        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Add Taxi Companies Data", null, null, 1.5f, null, cancelSrc.Token));

            if (Application.Current.Properties != null)
            {
                TaxiCompany1NameLabel.Text = Application.Current.Properties["Company1Name"].ToString();
                TaxiCompany1NumberLabel.Text = Application.Current.Properties["Company1PhoneNumer"].ToString().Trim();

                TaxiCompany2NameLabel.Text = Application.Current.Properties["Company2Name"].ToString();
                TaxiCompany2NumberLabel.Text = Application.Current.Properties["Company2PhoneNumer"].ToString().Trim();
            }
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

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                for (var counter = 1; counter < 3; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await Navigation.PopAsync();
            }
        }
        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Settings menu"
                    , null, null, 1.5f, null, cancelSrc.Token);
                flag = true;
            }
        }

        private void GetTaxiData()
        {
            Application.Current.Properties["Company1Name"] = TaxiCompany1NameLabel.Text;
            Application.Current.Properties["Company1PhoneNumer"] = TaxiCompany1NumberLabel.Text;
            Application.Current.Properties["Company2Name"] = TaxiCompany2NameLabel.Text;
            Application.Current.Properties["Company2PhoneNumer"] = TaxiCompany2NumberLabel.Text;
        }

        private void SaveData(object sender, EventArgs e)
        {
            GetTaxiData();
        }

        async void GoToSettingsNavigation(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new SettingsNavigation());
            }
        }
    }
}