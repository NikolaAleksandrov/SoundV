using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Models;
using XFApp1.ViewModels;

using Plugin.TextToSpeech;
namespace XFApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private bool flag;
        ItemsViewModel viewModel;
        public Home()
		{
            
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
            Title = "Home";
            flag = true;
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            flag = true;
            CrossTextToSpeech.Current.Speak("Welcome to Home Page");

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
               await Navigation.PushAsync(new CallPage());
            }
        }

        async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new NavigateToHomePage());
            }
        }
    }
}
