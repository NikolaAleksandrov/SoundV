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
using XFApp1.Interface;
using Plugin.Settings;

namespace XFApp1.Views.Taxi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxiCompanyPage : ContentPage
    {
        private bool flag;
        string taxiCompanyName = string.Empty;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public TaxiCompanyPage()
        {
            InitializeComponent();
            flag = true;
            NavigationPage.SetHasNavigationBar(this, false);
            taxiCompanyName = CrossSettings.Current.GetValueOrDefault("Company1Name", "No name");
            TaxiCompanyLabel.Text = taxiCompanyName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            //TODO: call company from view model + speak it
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Разговор с" + TaxiCompanyLabel.Text, null, null, null, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        //private void ReadPageText(object sender, EventArgs e)
        //{
        //    cancelSrc = new CancellationTokenSource();
        //    //taxiCompanyName = Application.Current.Properties["Company1Name"].ToString();
        //    taxiCompanyName = CrossSettings.Current.GetValueOrDefault("Company1Name", "No name");
        //    CrossTextToSpeech.Current.Speak("Разговор с " + taxiCompanyName, null, null, null, null, cancelSrc.Token);
        //}

        private void Call(object sender, EventArgs e)
        {
            //var number = Application.Current.Properties["Company1PhoneNumer"];
            var number = CrossSettings.Current.GetValueOrDefault("Company1PhoneNumer", "0000");
            DependencyService.Get<IMakePhoneCall>().MakeQuickCall(number);
        }
        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PopAsync();
            }
        }
        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await CrossTextToSpeech.Current.Speak("Няма страници в тази посока."
                    , null, null, null, null, cancelSrc.Token);
                flag = true;
            }
        }

        async void GoToTaxiCompany2(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new TaxiCompany2Page());
            }
        }
    }
}