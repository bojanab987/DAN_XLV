using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class EmployeeViewModel:ViewModelBase
    {
        EmployeeView employeeView;
        Service service = new Service();

        #region Constructor
        public EmployeeViewModel(EmployeeView view)
        {
            employeeView = view;
            ProductList = service.GetAllProducts().ToList();
        }
        #endregion

        #region Properties
        private List<tblProduct> productList;
        public List<tblProduct> ProductList
        {
            get { return productList; }
            set
            {
                productList = value;
                OnPropertyChanged("ProductList");
            }
        }

        private tblProduct product;
        public tblProduct Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private Visibility productView = Visibility.Visible;
        public Visibility ProductView
        {
            get
            {
                return productView;
            }
            set
            {
                productView = value;
                OnPropertyChanged("ProductView");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command for storing product
        /// </summary>
        private ICommand storeProduct;
        public ICommand StoreProduct
        {
            get
            {
                if (storeProduct == null)
                {
                    storeProduct = new RelayCommand(param => StoreProductExecute(), param => CanStoreProductExecute());
                }
                return storeProduct;
            }
        }

        /// <summary>
        /// Executes the Store Product command
        /// </summary>
        public void StoreProductExecute()
        {
            try
            {
                if (Product != null)
                {
                    int productId = Product.ID;
                    if (Product.Quantity>100)
                    {
                        service.NotifyEmployee(MessageBox.Show("Can not be stored more than 100 pieces of " + Product.ProductName + ", product with code:" + Product.ProductCode + "."));
                        
                    }
                    else if(Product.Stored=="yes")
                    {
                        service.NotifyEmployee(MessageBox.Show("Product already stored cannot be stored again."));
                    }
                    else
                    {
                        service.StoreProduct(Product);                       
                        service.NotifyEmployee(MessageBox.Show("Stored " + Product.Quantity+" pieces of " + Product.ProductName + ", product with code: " + Product.ProductCode + "."));
                        ProductList = service.GetAllProducts().ToList();                        
                    }  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the store product can be executed
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanStoreProductExecute()
        {
            return true;
        }


        private ICommand logOut;
        /// <summary>
        /// logout command
        /// </summary>
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        /// <summary>
        /// logout execute
        /// </summary>
        private void LogOutExecute()
        {
            LogInView log = new LogInView();
            log.Show();
            employeeView.Close();
        }

        /// <summary>
        /// Can logout execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanLogOutExecute()
        {
            return true;
        }
        #endregion
    }
}
