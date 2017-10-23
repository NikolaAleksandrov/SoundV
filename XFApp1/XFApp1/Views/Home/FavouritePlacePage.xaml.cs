using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritePlacePage : ContentPage
    {
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        bool flag;
        public FavouritePlacePage()
        {
            InitializeComponent();
            CrossTextToSpeech.Current.Speak("Go to my favourite place!", null, null, 1.5f,null, cancelSrc.Token);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            cancelSrc.Cancel();
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