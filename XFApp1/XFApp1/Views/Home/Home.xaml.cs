using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.ViewModels;

using Plugin.TextToSpeech;
using XFApp1.Views.Call;
using System.Threading;

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
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Welcome to home page", null, null, 1.5f, null, cancelSrc.Token));


            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
