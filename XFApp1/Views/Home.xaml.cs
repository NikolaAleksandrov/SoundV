using System;

using SoundV.Models;
using SoundV.ViewModels;

using Xamarin.Forms;
using Plugin.TextToSpeech;

namespace SoundV.Views
{
	public partial class Home : ContentPage
	{
		ItemsViewModel viewModel;
        int tapCount = 0;

        public Home()
		{
			InitializeComponent();
			BindingContext = viewModel = new ItemsViewModel();
            Title = "Home";
        }

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Item;
			if (item == null)
				return;

			await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

			// Manually deselect item
			//ItemsListView.SelectedItem = null;
		}

		async void Clock_Clicked(object sender, EventArgs e)
		{
            tapCount++;
            if (tapCount > 2)
            {
                await Navigation.PushAsync(new ClockTimePage());
                Title = "Clock";
            }
            else
            {
                await CrossTextToSpeech.Current.Speak("Clock Page");
            }
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
            
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
