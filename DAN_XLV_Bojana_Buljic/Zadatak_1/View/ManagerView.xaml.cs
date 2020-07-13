using System.Windows;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView()
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this);
        }
    }
}
