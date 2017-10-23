﻿using Plugin.TextToSpeech;
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
    public partial class CallTrustedPersonPage : ContentPage
    {
        bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public CallTrustedPersonPage()
        {
            InitializeComponent();
            flag = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            CrossTextToSpeech.Current.Speak("Would you like to call Trusted person 1", null, null, 1.5f, null, cancelSrc.Token);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            cancelSrc.Cancel();
        }

        async void ClearNavigationStack(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopToRootAsync();
            }
        }

        async void VideoCall(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new VideoCallPage());
            }
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Home page");
            }
        }
    }
}