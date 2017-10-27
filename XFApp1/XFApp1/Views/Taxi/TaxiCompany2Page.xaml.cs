using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Interface;

namespace XFApp1.Views.Taxi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxiCompany2Page : ContentPage
    {
        private bool flag;
        string taxiCompany2Name = string.Empty;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public TaxiCompany2Page()
        {
            InitializeComponent();
            flag = true;
            NavigationPage.SetHasNavigationBar(this, false);
        }
       
        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();

            taxiCompany2Name = Application.Current.Properties["Company2Name"].ToString();
            TaxiCompanyLabel.Text = taxiCompany2Name;
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Call: " + taxiCompany2Name, null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Call: " + TaxiCompanyLabel.Text, null, null, 1.5f, null, cancelSrc.Token);
        }

        private void Call(object sender, EventArgs e)
        {
            //TODO: get number from VM
            var number = Application.Current.Properties["Company2PhoneNumer"];
            DependencyService.Get<IMakePhoneCall>().MakeQuickCall(number.ToString());
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                for (var counter = 1; counter < 2; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await Navigation.PopAsync();
            }
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Call menu"
                    , null, null, 1.5f, null, cancelSrc.Token);
                flag = true;
            }
        }

        async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PopAsync();
            }
        }

    }
}