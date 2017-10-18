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
    
		ItemsViewModel viewModel;
        private int count = 1;
        int tapCount = 0;

        public Home()
		{
            count = Navigation.NavigationStack.Count;
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
            Title = "Home";
        }

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Item;
			if (item == null)
				return;

			// Manually deselect item
			//ItemsListView.SelectedItem = null;
		}
        private bool flag = true;
		async void GoToCallPage(object sender, EventArgs e)
		{
           
            if (flag)
            {
                flag = false;
                    count = Navigation.NavigationStack.Count;
               await Navigation.PushAsync(new CallPage());
            }
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
            tapCount = 0;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CrossTextToSpeech.Current.Speak("Clock Page");
        }
    }
    }
