using Plugin.TextToSpeech;
using SoundV.ViewModels;
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
    public partial class SettingsCustomize : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public SettingsCustomize()
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
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Add Trusted Person Data", null, null, 1.5f, null, cancelSrc.Token));

            if(Application.Current.Properties != null)
            {
                TrustedPersonNameLabel.Text = Application.Current.Properties["TrustedPersonName"].ToString();
                TrustedPersonNumberLabel.Text = Application.Current.Properties["TrustedPersonPhoneNumber"].ToString().Trim();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
        async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PopAsync();
            }
        }

        private void GetTrustedPersonData()
        { 
            Application.Current.Properties["TrustedPersonName"] = TrustedPersonNameLabel.Text;
            Application.Current.Properties["TrustedPersonPhoneNumber"] = TrustedPersonNumberLabel.Text.Trim();
        }

        private void SaveData(object sender, EventArgs e)
        {
            GetTrustedPersonData();
        }

        async void GoToSettingsTaxi(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new SettingsTaxi());
            }
        }
    }
}