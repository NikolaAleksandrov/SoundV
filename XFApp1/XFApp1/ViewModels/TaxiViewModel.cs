using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SoundV.ViewModels
{
    class TaxiViewModel
    {
        public string CompanyName { get; set; }
        public int CompanyPhoneNumer { get; set; }

        public TaxiViewModel()
        {
            this.CompanyName = string.Empty;
            this.CompanyPhoneNumer = 0;
        }
    }
}
