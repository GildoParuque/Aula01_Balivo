using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Aula01_Balivo.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    if (PropertyChanged is null)
        //        return;
        //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
          
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool _IsBusy = false;
        public bool isBusy
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy { get => !_IsBusy; }
    }
}
 