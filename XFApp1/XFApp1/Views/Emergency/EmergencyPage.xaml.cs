﻿using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFApp1.Views.Taxi;

namespace XFApp1.Views.Emergency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergencyPage : ContentPage
    {
        private bool flag;
        public EmergencyPage()
        {
            InitializeComponent();
            flag = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrossTextToSpeech.Current.Speak("Call an emergency number");
            flag = true;
        }

        private async void GoToSubLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new EmergencySubLevel());
            }
        }

        private async void PreviousPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopAsync();
            }
        }

        private async void GoToTaxiPage(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PushAsync(new TaxiPage());
            }
        }

    }
}