using System.Windows;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        public LogInView()
        {
            InitializeComponent();
            this.DataContext = new LogInViewModel(this);
        }

        /// <summary>
        /// Disable login button unil both field are not empty
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs event</param>
        private void txtChanged(object sender, RoutedEventArgs e)
        {

            if (passwordBox.Password.Length > 0 && txtUsername.Text.Length > 0)
            {
                btnLogin.IsEnabled = true;
            }
            else
            {
                btnLogin.IsEnabled = false;
            }
        }
    }
}
