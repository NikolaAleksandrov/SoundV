﻿using System;
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

namespace XFApp1.Views.Taxi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxiCompanyPage : ContentPage
    {
        private bool flag;
        CancellationTokenSource cancelSrc = new CancellationTokenSource();
        public TaxiCompanyPage()
        {
            InitializeComponent();
            flag = true;
            NavigationPage.SetHasNavigationBar(this, false);
            TaxiCompanyLabel.Text = Application.Current.Properties["Company1Name"].ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            flag = true;
            cancelSrc = new CancellationTokenSource();
            //TODO: call company from view model + speak it
            Task.Run(async () => await CrossTextToSpeech.Current.Speak("Call" + TaxiCompanyLabel.Text, null, null, 1.5f, null, cancelSrc.Token));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ReadPageText(object sender, EventArgs e)
        {
            cancelSrc = new CancellationTokenSource();
            //TODO: get name from ViewModel
            CrossTextToSpeech.Current.Speak("Call: ", null, null, 1.5f, null, cancelSrc.Token);
        }

        private void Call(object sender, EventArgs e)
        {
            //TODO: Get number from view model
            DependencyService.Get<IMakePhoneCall>().MakeQuickCall("123");
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
                cancelSrc.Cancel();
                cancelSrc.Dispose();
                await CrossTextToSpeech.Current.Speak("There are no pages in that direction. Please swipe down to Taxi page");
                flag = true;
            }
        }
    }
}