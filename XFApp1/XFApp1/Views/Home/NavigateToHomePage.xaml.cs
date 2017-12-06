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
    public partial class NavigateToHomePage : ContentPage
    {
        bool flag;
        int count = 0;
        string homePlaceAddress = string.Empty;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public NavigateToHomePage()
        {
            InitializeComponent();

            flag = true;
            count = 1;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            count = count + 1;
            cancelSrc = new CancellationTokenSource();
            //TO DO: add home address and read it (concatenation)
            homePlaceAddress = Application.Current.Properties["HomePlace"].ToString();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Отиди до" + homePlaceAddress, null, null, 1.5f, null, cancelSrc.Token));

            GoHomeLabel.Text = Application.Current.Properties["HomePlace"].ToString(); 
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        async void GotoFavouritePlace(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PushAsync(new FavouritePlacePage());
            }
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PopToRootAsync();
            }
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await CrossTextToSpeech.Current.Speak("Няма страници в тази посока."
                    , null, null, 1.5f, null, cancelSrc.Token);
                flag = true;
            }
        }
    }
}