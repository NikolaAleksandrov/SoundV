using XFApp1.Helpers;
using XFApp1.Models;
using XFApp1.Services;

using Xamarin.Forms;
using System.ComponentModel;

namespace XFApp1.ViewModels
{
	public class BaseViewModel : ObservableObject, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

		bool isBusy = false;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}
		/// <summary>
		/// Private backing field to hold the title
		/// </summary>
		string title = string.Empty;
		/// <summary>
		/// Public property to set and get the title of the item
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

    }
}

