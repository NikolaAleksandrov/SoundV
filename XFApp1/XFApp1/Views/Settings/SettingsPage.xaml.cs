using Plugin.TextToSpeech;
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
    public partial class SettingsPage : ContentPage
    {
        private bool flag;
        private bool cautionFlag;
        CancellationTokenSource cancelSrc;
        public SettingsPage()
        {
            InitializeComponent();
            flag = true;
            cautionFlag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Настройки. Моля, използвайте с асистент", null, null, null, null, cancelSrc.Token));
            flag = true;
            cautionFlag = true;
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Настройки. Използвайте с асистент", null, null, null, null, cancelSrc.Token);
        }

        private void CautionMessage(object sender, EventArgs e)
        {
            if (cautionFlag && flag)
            {
                cautionFlag = false;
                flag = false;
                cancelSrc = new CancellationTokenSource();
                Task.Run(async () => { await CrossTextToSpeech.Current.Speak("Няма страници в тази посока.", null, null, null, null, cancelSrc.Token); cautionFlag = true; });
                flag = true;
            }
        }

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PopAsync();
            }
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Dispose();
                await Navigation.PopToRootAsync();
            }
        }

        private async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new SettingsCustomize());
            }
        }
    }
}