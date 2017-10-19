using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritePlacePage : ContentPage
    {
        bool flag;
        public FavouritePlacePage()
        {
            InitializeComponent();
            CrossTextToSpeech.Current.Speak("Go to my favourite place!");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
        }

        async void PreviousPage(object sender, EventArgs e)
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
    }
}