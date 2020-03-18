
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KsondzykLab2.Annotations;

namespace KsondzykLab2.Tools
{
    internal class BaseViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
        #endregion
}
