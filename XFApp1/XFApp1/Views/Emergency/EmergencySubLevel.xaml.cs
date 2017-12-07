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
using XFApp1.Views.Call;

namespace XFApp1.Views.Emergency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencySubLevel : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public EmergencySubLevel()
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
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Разговор със спешна помощ", null, null, null, null,cancelSrc.Token));
        }

        //private void ReadPageText(object sender, EventArgs e)
        //{
        //    cancelSrc = new CancellationTokenSource();
        //    CrossTextToSpeech.Current.Speak("Обадете се на 112", null, null, 1.5f, null, cancelSrc.Token);
        //}

        private void Call(object sender, EventArgs e)
        {
            var number = "112";
            DependencyService.Get<IMakePhoneCall>().MakeQuickCall(number.ToString());
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopAsync();
            }
        }
        private void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc = new CancellationTokenSource();
                Task.Run(async () =>
                {
                    await CrossTextToSpeech.Current.Speak("Няма страници в тази посока.", null, null, null, null, cancelSrc.Token);
                    flag = true;
                });
                
            }
        }
    }
}