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
            cancelSrc = new CancellationTokenSource();
            //TODO: get battery level from viewmodel
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Battery level menu: ", null, null, 1.5f, null, cancelSrc.Token));
            flag = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            //TODO:Battery level
            CrossTextToSpeech.Current.Speak("Binding batery level", null, null, 1.5f, null, cancelSrc.Token);
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
                cancelSrc = null;
                flag = false;
                await Navigation.PushAsync(new SettingsPage());
            }
        }
    }
}