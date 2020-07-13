using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for AddEditProduct.xaml
    /// </summary>
    public partial class AddEditProduct : Window
    {
        /// <summary>
        /// Constructor for view for adding new products
        /// </summary>
        public AddEditProduct()
        {
            InitializeComponent();
            this.DataContext = new AddEditProductViewModel(this);
        }

        /// <summary>
        /// Constructor for view for editing products
        /// </summary>
        /// <param name="productEdit">product that is bing edited</param>
        public AddEditProduct(tblProduct productEdit)
        {
            InitializeComponent();
            this.DataContext = new AddEditProductViewModel(this, productEdit);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
