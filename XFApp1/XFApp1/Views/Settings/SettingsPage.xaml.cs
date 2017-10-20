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
    public partial class SettingsPage : ContentPage
    {
        private bool flag;
        public SettingsPage()
        {
            InitializeComponent();
            flag = true;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossTextToSpeech.Current.Speak("Please call an assistant to use this page");
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

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopToRootAsync();
            }
        }

        private async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new SettingsCustomize());
            }
        }
    }
}