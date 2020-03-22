using System.Windows;
using Lab4_CSHARP.Tools.DataStorage;
using Lab4_CSHARP.Tools.Managers;
using Lab4_CSHARP.ViewModels;

namespace Lab4_CSHARP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StationManager.Initialize(new SerializedDataStorage());
            DataContext = new MainWindowViewModel();
        }

    }
}
