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
    public partial class Page1 : ContentPage
    {
    
		ItemsViewModel viewModel;
        private int count = 1;
        int tapCount = 0;

        public Page1()
		{
            count = Navigation.NavigationStack.Count;
            InitializeComponent();
            SwipeableImage.SwipedUp += (sender, args) => { Info.Text = "UP"; };
            SwipeableImage.SwipedDown += (sender, args) => { Info.Text = "DOWN"; };
            
            SwipeableImage.SwipedRight += (sender, args) => { Info.Text = "RIGHT"; };
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
        private bool flag = true;
		async void Clock_Clicked(object sender, EventArgs e)
		{
           
            if (flag)
            {
                flag = false;
                    count = Navigation.NavigationStack.Count;
               await Navigation.PushAsync(new Page2());
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
