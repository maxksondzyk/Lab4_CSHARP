using System.ComponentModel;
using System.Windows;

namespace Lab4_CSHARP.Tools
{
    interface ILoaderOwner:INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }

}
