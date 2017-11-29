using Android.Content;
using Plugin.Settings;
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

namespace XFApp1.Views.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallTrustedPersonPage : ContentPage
    {
        bool flag;
        string trustedPersonName = string.Empty;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public CallTrustedPersonPage()
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
            //trustedPersonName = Application.Current.Properties["TrustedPersonName"].ToString();
            trustedPersonName = CrossSettings.Current.GetValueOrDefault("TrustedPersonName", "No name");

            TrustedPersonLabel.Text = trustedPersonName;
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Call: " + trustedPersonName, null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak(TrustedPersonLabel.Text, null, null, 1.5f, null, cancelSrc.Token);
        }

        private void Call(object sender, EventArgs e)
        {
            //TODO: get number from VM
            //var number = Application.Current.Properties["TrustedPersonPhoneNumber"];
            var number = CrossSettings.Current.GetValueOrDefault("TrustedPersonPhoneNumber", "0000");
            DependencyService.Get<IMakePhoneCall>().MakeQuickCall(number);
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

        async void VideoCall(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new VideoCallPage());
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
    }
}