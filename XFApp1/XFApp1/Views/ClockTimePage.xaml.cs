﻿using Plugin.TextToSpeech;
using SoundV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockTimePage : ContentPage
    {
        string currentTime = string.Empty;

        bool flag;
        CancellationTokenSource cancelSrc;
        public ClockTimePage()
        {
            InitializeComponent();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            NavigationPage.SetHasNavigationBar(this, false);
            currentTime = DateTime.Now.ToString("HH:mm");
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Welcome to Sound Vision App. You can swipe right or left for navigating" +
                "to different pages. Your are currently at the Clock Time Page. Double tap to check the time", null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnAppearing()
        {
            currentTime = DateTime.Now.ToString("HH:mm");
            TellTheTimeLabel.Text = currentTime;
            base.OnAppearing();
            cancelSrc = new CancellationTokenSource();
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Check the time", null, null, 1.5f, null, cancelSrc.Token));
            flag = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("Double tap to check the time", null, null, 1.5f, null, cancelSrc.Token);
        }

        async void CautionMessage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Dispose();
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe right");
                flag = true;
            }
        }

        private void TellTheTime(object sender, EventArgs e)
        {
            var currentTime = DateTime.Now.ToString("HH:mm");

            TellTheTimeLabel.Text = currentTime;
            cancelSrc = new CancellationTokenSource();
            CrossTextToSpeech.Current.Speak("The time is" + currentTime, null, null, 1.0f, null, cancelSrc.Token);
        }

        private async void GoToDate(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await Navigation.PushAsync(new DateTimePage());
            }
        }
    }
}

