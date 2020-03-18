
using System.Windows.Controls;
using KsondzykLab2.ViewModels;

namespace KsondzykLab2.Views
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
