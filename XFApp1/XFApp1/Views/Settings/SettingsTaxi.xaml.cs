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
        private bool cautionFlag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public SettingsTaxi()
        {
            InitializeComponent();
            flag = true;
            cautionFlag = true;
            NavigationPage.SetHasNavigationBar(this, false);
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cautionFlag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Добави такси компании", null, null, 1.5f, null, cancelSrc.Token));

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
                for (var counter = 1; counter < 2; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await Navigation.PopAsync();
            }
        }
        private void CautionMessage(object sender, EventArgs e)
        {
            if (cautionFlag && flag)
            {
                cautionFlag = false;
                cancelSrc = new CancellationTokenSource();
                Task.Run(async () => { await CrossTextToSpeech.Current.Speak("Няма страници в тази посока.", null, null, null, null, cancelSrc.Token); cautionFlag = true; });
                flag = true;
            }
        }

        private void GetTaxiData()
        {
            CrossSettings.Current.AddOrUpdateValue("Company1Name", TaxiCompany1NameLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("Company1PhoneNumer", TaxiCompany1NumberLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("Company2Name", TaxiCompany2NameLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("Company2PhoneNumer", TaxiCompany2NumberLabel.Text);
            CrossTextToSpeech.Current.Speak("Запазено");
        }

        private void SaveData(object sender, EventArgs e)
        {
            GetTaxiData();
        }

    }
}