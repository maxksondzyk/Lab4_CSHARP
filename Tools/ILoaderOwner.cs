using System.ComponentModel;
using System.Windows;

namespace KsondzykLab2.Tools
{
    interface ILoaderOwner:INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }

}
