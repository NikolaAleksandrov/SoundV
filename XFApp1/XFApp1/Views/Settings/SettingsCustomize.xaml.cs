using Plugin.Settings;
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
        private bool cautionFlag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public SettingsCustomize()
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
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Добави любим човек.", null, null, null, null, cancelSrc.Token));


            TrustedPersonNameLabel.Text = CrossSettings.Current.GetValueOrDefault("TrustedPersonName", "No name");
            TrustedPersonNumberLabel.Text = CrossSettings.Current.GetValueOrDefault("TrustedPersonPhoneNumber", "0000");

        }

        async void ClearNavigationStack(object sender, EventArgs e)
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
            CrossSettings.Current.AddOrUpdateValue("TrustedPersonName", TrustedPersonNameLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("TrustedPersonPhoneNumber", TrustedPersonNumberLabel.Text);
            CrossTextToSpeech.Current.Speak("Запaзено.");
        }

        private void CautionMessage(object sender, EventArgs e)
        {
            if (cautionFlag && flag)
            {
                cautionFlag = false;
                flag = false;
                cancelSrc = new CancellationTokenSource();
                Task.Run(async () => { await CrossTextToSpeech.Current.Speak("Няма страници в тази посока.", null, null, null, null, cancelSrc.Token); cautionFlag = true; });
                flag = true;
            }
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