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

            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Батерия: ", null, null, null, null, cancelSrc.Token));

            batteryLevel = CrossBattery.Current.RemainingChargePercent.ToString();
            BatteryLevelLabel.Text = "Батерия " + batteryLevel + " процента";
        }

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
            BatteryLevelLabel.Text = "Батерия " + batteryLevel + " процента";
            if (int.Parse(batteryLevel) < 15)
            {
                Task.Run(async () => await CrossTextToSpeech.Current.Speak("Внимание, батерията ви е под петнадесет процента", null, null, null, null, cancelSrc.Token));
            }
            else
            {
                Task.Run(async () => await CrossTextToSpeech.Current.Speak("Батерия: " + batteryLevel + "процента", null, null, null, null, cancelSrc.Token)); ;
            }
        }
    }
}