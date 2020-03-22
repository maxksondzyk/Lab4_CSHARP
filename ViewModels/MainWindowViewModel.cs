using System.Windows;
using Lab4_CSHARP.Tools;
using Lab4_CSHARP.Tools.Managers;
using Lab4_CSHARP.Tools.Navigation;

namespace Lab4_CSHARP.ViewModels
{
    internal class MainWindowViewModel:BaseViewModel,ILoaderOwner, IContentOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        private INavigatable _content;
        #endregion

        #region Properties
        public INavigatable Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        internal MainWindowViewModel()
        {
            LoaderManager.Instance.Initialize(this);
            LoaderManager.Instance.Initialize(this);
            NavigationManager.Instance.Initialize(new ListNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.Main);
        }

    }
}
