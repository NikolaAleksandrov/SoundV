using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.ViewModels;

using Plugin.TextToSpeech;
using XFApp1.Views.Call;
using System.Threading;
using Plugin.Geolocator;

namespace XFApp1.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private bool flag;
        ItemsViewModel viewModel;
        CancellationTokenSource cancelSrc;
        public Home()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
            Title = "Home";
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            //TO DO: add current location from Services and then read it with the address (concatenation)
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Current location: ", null, null, 1.5f, null, cancelSrc.Token));


            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        async void ReadPageText(object sender, EventArgs e)
        {
            await CrossTextToSpeech.Current.Speak("Double tap to get location", null, null, 1.5f, null, cancelSrc.Token);
        }

        async void GetLocation(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            await Task.Run(async () => await CrossTextToSpeech.Current.Speak("Getting location.Please wait", null, null, 1.5f, null, cancelSrc.Token));
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 15;
            var position = await locator.GetPositionAsync();
            var address = await locator.GetAddressesForPositionAsync(position);

            AddressLabel.Text = address.FirstOrDefault().FeatureName + ", " +
                address.FirstOrDefault().Thoroughfare + ", " +
                address.FirstOrDefault().AdminArea + ", " +
                address.FirstOrDefault().CountryName;
            await CrossTextToSpeech.Current.Speak(AddressLabel.Text, null, null, 1.5f, null, cancelSrc.Token);
        }

        async void GoToCallPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PushAsync(new CallPage());
            }
        }

        async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PushAsync(new NavigateToHomePage());
            }
        }

        private void GoToSetting(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                //var a = Navigation.ModalStack.Last();
                //var b = a.GetType();
            }
        }
    }
}
