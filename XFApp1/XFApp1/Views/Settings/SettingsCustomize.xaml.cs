using Plugin.TextToSpeech;
using SoundV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsCustomize : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public SettingsCustomize()
        {
            InitializeComponent();
            flag = true;
            BindingContext = new TaxiViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Change your settings", null, null, 1.5f, null, cancelSrc.Token));
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
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Home page");
            }
        }

        private void GetTaxiData()
        {
            var vm = (this.BindingContext as TaxiViewModel);
            vm.CompanyName = TaxiCompanyName.Text;
            vm.CompanyPhoneNumer = Convert.ToInt32(TaxiCompanyNumber.Text);
        }

        private void SaveData(object sender, EventArgs e)
        {
            GetTaxiData();
        }
    }
}