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
using XFApp1.ViewModels;

namespace XFApp1.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsNavigation : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public SettingsNavigation()
        {
            InitializeComponent();
            flag = true;
            BindingContext = new HomeViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
 

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Change your settings", null, null, 1.5f, null, cancelSrc.Token));

            if (Application.Current.Properties != null)
            {
                HomePlaceLabel.Text = Application.Current.Properties["HomePlace"].ToString();

                SavedPlace1Label.Text = Application.Current.Properties["SavedPlace1"].ToString();
                SavedPlace2Label.Text = Application.Current.Properties["SavedPlace2"].ToString();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
        async void GoToSettingsTrustedPerson(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new SettingsCustomize());
            }
        }

        private void GetHomeData()
        {
            //Application.Current.Properties["HomePlace"] = HomePlaceLabel.Text;
            //Application.Current.Properties["SavedPlace1"] = SavedPlace1Label.Text;
            //Application.Current.Properties["SavedPlace2"] = SavedPlace2Label.Text;

            ////add some data to the local storage i guess
            CrossSettings.Current.AddOrUpdateValue("HomePlace", HomePlaceLabel.Text);
            CrossSettings.Current.AddOrUpdateValue("SavedPlace1", SavedPlace1Label.Text);
            CrossSettings.Current.AddOrUpdateValue("SavedPlace2", SavedPlace2Label.Text);


        }

        private void SaveData(object sender, EventArgs e)
        {
            GetHomeData();
        }
    }
}
