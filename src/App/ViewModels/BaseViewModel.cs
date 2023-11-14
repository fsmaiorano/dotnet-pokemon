using System.ComponentModel;

namespace App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //protected void SetProperty<T>(ref T backingStore, T value, string propertyName)
        //{
        //    if (Equals(backingStore, value))
        //        return;

        //    backingStore = value;
        //    OnPropertyChanged(propertyName);
        //}
    }
}
