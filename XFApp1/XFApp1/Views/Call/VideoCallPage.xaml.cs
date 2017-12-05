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

namespace XFApp1.Views.Call
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCallPage : ContentPage
    {
        bool flag;
        string trustedPersonName = string.Empty;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public VideoCallPage()
        {
            InitializeComponent();
            flag = true;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //trustedPersonName = Application.Current.Properties["TrustedPersonName"].ToString();
            
            trustedPersonName = CrossSettings.Current.GetValueOrDefault("TrustedPersonName", "No name");
            flag = true;
            TrustedPersonLabel.Text = trustedPersonName;
            cancelSrc = new CancellationTokenSource();
            //TODO: get name from viewModel
           
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Видео обаждане с: " + trustedPersonName, null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        async void PreviousPage(object sender, EventArgs e)
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

        async void CallAssistance(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new CallAssistance());
            }
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                for (var counter = 1; counter < 2; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await Navigation.PopAsync();
            }
        }
    }
}