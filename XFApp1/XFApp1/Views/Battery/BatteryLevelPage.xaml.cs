using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Views.Settings;

namespace XFApp1.Views.Battery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BatteryLevelPage : ContentPage
    {
        private bool flag;
        public BatteryLevelPage()
        {
            InitializeComponent();
            flag = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossTextToSpeech.Current.Speak("Check your battery level");
            flag = true;
        }

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopAsync();
            }
        }

        private async void GoToSettings(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new SettingsPage());
            }
        }
    }
}