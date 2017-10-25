using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XFApp1.Droid;
using XFApp1.Interface;

[assembly: Dependency(typeof (MakePhoneCall))]
namespace XFApp1.Droid
{
    class MakePhoneCall : IMakePhoneCall
    {
        public void MakeQuickCall(string PhoneNumber)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", PhoneNumber));
                var intent = new Intent(Intent.ActionCall, uri);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}