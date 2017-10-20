using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsCustomize : ContentPage
    {
        private bool flag;
        public SettingsCustomize()
        {
            InitializeComponent();
            flag = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            CrossTextToSpeech.Current.Speak("Change your settings");
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopToRootAsync();
            }
        }
        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Home page");
            }
        }
    }
}