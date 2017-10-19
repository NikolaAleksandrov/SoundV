using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallPage : ContentPage
    {
        bool flag;
        public CallPage()
        {
            InitializeComponent();
            flag = true;
            CrossTextToSpeech.Current.Speak("Emergency call");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Navigation.NavigationStack.Count < 3)
            {
                CrossTextToSpeech.Current.Speak("Emergency call");
            }
            flag = true;
        }

        private async void GoToTaxiPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new TaxiPage());
            }
        }

        private async void PreviousPageMainLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopAsync();
            }
        }
    }
}