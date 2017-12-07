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
using XFApp1.ViewModels;
using XFApp1.Views.Call;

namespace XFApp1.Views.Emergency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencySubLevel : ContentPage
    {
        private bool flag;
        private bool cautionFlag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();

        public EmergencySubLevel()
        {
            InitializeComponent();
            flag = true;
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new EmergencyViewModel();
          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cautionFlag = true;
            var previous = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2].GetType();
            var a = new EmergencyViewModel();
            if (previous == typeof(CallAssistance))
            {
                a.Message = "Предишна страница- разговор с поддръжка";
            }
            else
            {
                a.Message = "Няма страници в тази посока";
            }
            this.BindingContext = a;
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
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                var previous = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2].GetType();
                if (previous == typeof(CallAssistance))
                {
                    for (var counter = 1; counter < 4; counter++)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    }
                }

                await Navigation.PopAsync();

            }
        }
        private void CautionMessage(object sender, EventArgs e)
        {
            if (cautionFlag)
            {
                cautionFlag = false;
                cancelSrc = new CancellationTokenSource();
                Task.Run(async () => { await CrossTextToSpeech.Current.Speak("Няма страници в тази посока. Моля, плъзнете надолу", null, null, null, null, cancelSrc.Token); cautionFlag = true; });

            }
        }
        private void SwipeLeft(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                var previous = Navigation.NavigationStack[(Navigation.NavigationStack.Count) - 2].GetType();
                if (previous == typeof(CallAssistance))
                {
                    Navigation.PopAsync();
                }
                else
                {
                    cancelSrc = new CancellationTokenSource();
                    Task.Run(async () => { await CrossTextToSpeech.Current.Speak("Няма страници в тази посока. Моля, плъзнете надолу", null, null, null, null, cancelSrc.Token); cautionFlag = true; });
                }
            }
        }
    }
}