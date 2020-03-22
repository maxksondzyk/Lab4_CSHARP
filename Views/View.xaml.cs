﻿using System.Windows.Controls;
using Lab4_CSHARP.Tools.Navigation;
using Lab4_CSHARP.ViewModels;

namespace Lab4_CSHARP.Views
{
    public partial class View : UserControl,INavigatable
    {
        public View()
        {
            InitializeComponent();
            DataContext = ViewModel.Instance;
        }

    }
}
