using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class AddEditProductViewModel:ViewModelBase
    {
        AddEditProduct addView;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor for openning window for adding new product
        /// </summary>
        /// <param name="addOpen"></param>
        public AddEditProductViewModel(AddEditProduct addOpen)
        {
            addView = addOpen;
            Product = new tblProduct();
            ProductList = service.GetAllProducts().ToList();            
        }

        /// <summary>
        /// Constructor for openning window for editing product
        /// </summary>
        /// <param name="addOpen">AddEditProduct View</param>
        /// <param name="editProduct">product for editing</param>
        public AddEditProductViewModel(AddEditProduct addOpen, tblProduct editProduct)
        {
            addView = addOpen;
            Product = editProduct;
            ProductList = service.GetAllProducts().ToList();
        }
        #endregion

        #region Properties
        private tblProduct product;
        public tblProduct Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private List<tblProduct> productList;
        public List<tblProduct> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
                OnPropertyChanged("ProductList");
            }
        }

        /// <summary>
        /// Checks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateProduct;
        public bool IsUpdateProduct
        {
            get
            {
                return isUpdateProduct;
            }
            set
            {
                isUpdateProduct = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Save Command
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddEditProduct(Product);
                IsUpdateProduct = true;
                //closing the addEditProduct view
                addView.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute saving of the product
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Product.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add or edit window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
