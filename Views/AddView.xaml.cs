using System.Windows.Controls;
using Lab4_CSHARP.Tools.Navigation;
using Lab4_CSHARP.ViewModels;

namespace Lab4_CSHARP.Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl,INavigatable
    {
        public AddView()
        {
            InitializeComponent();
            DataContext = ViewModel.Instance;
        }
    }
}
