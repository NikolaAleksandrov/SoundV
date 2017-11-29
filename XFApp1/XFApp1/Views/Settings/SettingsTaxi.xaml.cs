using Plugin.Settings;
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

            TaxiCompany1NameLabel.Text = CrossSettings.Current.GetValueOrDefault("Company1Name", "No name");
            TaxiCompany1NumberLabel.Text = CrossSettings.Current.GetValueOrDefault("Company1PhoneNumer", "0000");

            TaxiCompany2NameLabel.Text = CrossSettings.Current.GetValueOrDefault("Company2Name", "No name");
            TaxiCompany2NumberLabel.Text = CrossSettings.Current.GetValueOrDefault("Company2PhoneNumer", "0000");

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
            //Application.Current.Properties["Company1Name"] = TaxiCompany1NameLabel.Text;
            //Application.Current.Properties["Company1PhoneNumer"] = TaxiCompany1NumberLabel.Text;
            //Application.Current.Properties["Company2Name"] = TaxiCompany2NameLabel.Text;
            //Application.Current.Properties["Company2PhoneNumer"] = TaxiCompany2NumberLabel.Text;

            //add data to local storage i guess
            CrossSettings.Current.AddOrUpdateValue("Company1Name", TaxiCompany1NameLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("Company1PhoneNumer", TaxiCompany1NumberLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("Company2Name", TaxiCompany2NameLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("Company2PhoneNumer", TaxiCompany2NumberLabel.Text);
            CrossTextToSpeech.Current.Speak("Saved");
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