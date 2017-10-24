using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.TextToSpeech;
using System.Threading;
using SoundV.ViewModels;

namespace XFApp1.Views.Taxi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxiCompanyPage : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public TaxiCompanyPage()
        {
            InitializeComponent();
            flag = true;
            this.BindingContext = new TaxiViewModel();
        }

        protected override void OnAppearing()
        {
            var vm = this.BindingContext as TaxiViewModel;
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Double tap to call" + vm.CompanyName, null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PopAsync();
            }
        }
        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Taxi page");
            }
        }
    }
}