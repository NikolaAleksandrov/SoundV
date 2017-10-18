using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxiPage : ContentPage
    {
        bool flag;
        public TaxiPage()
        {
            InitializeComponent();
            flag = true;
            CrossTextToSpeech.Current.Speak("Would you like to call a taxi?");
        }

        private async void PreviousPageMainLevel(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                await Navigation.PopAsync();
            }
        }
    }
}