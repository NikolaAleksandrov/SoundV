using System;
using System.Collections.Generic;
using System.Text;

namespace SoundV.ViewModels
{
    class CallViewModel
    {
        public string TrustedPersonName { get; set; }
        public string TrustedPersonPhoneNumber { get; set; }
        public CallViewModel()
        {
            TrustedPersonName = "";
            TrustedPersonPhoneNumber = "";

        }
    }
}
