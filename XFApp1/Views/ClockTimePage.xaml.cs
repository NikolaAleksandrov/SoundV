using Plugin.TextToSpeech;
using SoundV.ViewModels;
using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoundV.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClockTimePage : ContentPage
	{
        ClockTimeViewModel viewModel;
        string requestedTime = string.Empty;

        public ClockTimePage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new ClockTimeViewModel();
            viewModel.currentTime = DateTime.Now;            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.currentTime = DateTime.Now;
            CrossTextToSpeech.Current.Speak("Welcome to Clock Page");
        }

        async void ClockTime_Requested(object sender, EventArgs e)
        {
            viewModel.currentTime = DateTime.Now;
            requestedTime = viewModel.currentTime.ToShortTimeString();
            await CrossTextToSpeech.Current.Speak(requestedTime);
         
        }

        private void OpenNewPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DateTimePage());
        }
    }
}