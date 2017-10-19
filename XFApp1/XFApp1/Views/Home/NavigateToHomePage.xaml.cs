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
    public partial class NavigateToHomePage : ContentPage
    {
        bool flag;
        public NavigateToHomePage()
        {
            InitializeComponent();

            flag = true;
            CrossTextToSpeech.Current.Speak("Navigate me to home");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
        }

        async void GotoFavouritePlace(object sender, EventArgs e)
        {
            if (flag)
            {
                //await Navigation.PushAsync();
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
    }
}