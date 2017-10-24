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
    public partial class CallAssistance : ContentPage
    {
        bool flag;
        CancellationTokenSource cancelSrc;
        public CallAssistance()
        {
            InitializeComponent();
            flag = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Do you need any help? Double tap to call an assistant", null, null, 1.5f, null, cancelSrc.Token));
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

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                cancelSrc = null;
                await Navigation.PopToRootAsync();
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
    }
}