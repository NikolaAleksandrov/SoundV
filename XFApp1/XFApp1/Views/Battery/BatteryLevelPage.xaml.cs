using Plugin.Battery;
using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Interface;
using XFApp1.Views.Settings;

namespace XFApp1.Views.Battery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BatteryLevelPage : ContentPage
    {
        string batteryLevel = string.Empty;
        private bool flag;
        CancellationTokenSource cancelSrc;
        public BatteryLevelPage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            //TODO: get battery level from viewmodel

            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Батерия: ", null, null, 1.5f, null, cancelSrc.Token));

            batteryLevel = CrossBattery.Current.RemainingChargePercent.ToString();
            BatteryLevelLabel.Text = batteryLevel + "%";
        }

        //private void ReadPageText(object sender, EventArgs e)
        //{
        //    cancelSrc = new CancellationTokenSource();
        //    //TODO:Battery level
        //    batteryLevel = CrossBattery.Current.RemainingChargePercent.ToString();
        //    CrossTextToSpeech.Current.Speak("Get battery level", null, null, 1.5f, null, cancelSrc.Token);
        //}

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                flag = false;
                await Navigation.PopAsync();
            }
        }

        private async void GoToSettings(object sender, EventArgs e)
        {
            if (flag)
            {
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                flag = false;
                await Navigation.PushAsync(new SettingsPage());
            }
        }

        private void GetBatteryLevel(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();

            batteryLevel = CrossBattery.Current.RemainingChargePercent.ToString();
            if (int.Parse(batteryLevel) < 15)
            {
                CrossTextToSpeech.Current.Speak("Внимание, батерията ви е под 15 процента", null, null, 1.5f, null, cancelSrc.Token);
            }
            else
            {
                CrossTextToSpeech.Current.Speak("Батерия: " + batteryLevel + "процента", null, null, 1.5f, null, cancelSrc.Token);
            }
        }
    }
}