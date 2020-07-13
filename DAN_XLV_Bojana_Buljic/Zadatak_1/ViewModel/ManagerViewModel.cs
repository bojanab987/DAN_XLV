using System;
using System.Collections.Generic;
using System.Linq;
using Zadatak_1.View;
using Zadatak_1.Services;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class ManagerViewModel:ViewModelBase
    {
        ManagerView managerView;
        Service service;

        public ManagerViewModel(ManagerView managerOpen)
        {
            managerView = managerOpen;
            service = new Service();
            ProductList = service.GetAllProducts().ToList();
        }

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
        private ICommand delete;
        /// <summary>
        /// delete button command
        /// </summary>
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteExecute(), param => CanDeleteExecute());
                }
                return delete;
            }
        }

        /// <summary>
        /// delete order execute
        /// </summary>
        private void DeleteExecute()
        {
            var result = MessageBox.Show("Are you sure you want to delete the product?", "Confirmation", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Product != null)
                    {
                        
                        int productId = Product.ID;
                        if (Product.Stored == "yes")
                        {
                            MessageBox.Show("Product is stored and cannot be deleted from database.");
                            service.Notify("Could not delete Product: " + Product.ProductName + " with code: " + Product.ProductCode + " since its stored.");
                        }
                        else
                        {
                            
                            service.DeleteProduct(productId);
                            MessageBox.Show("Product " + Product.ProductName + " with code:" + Product.ProductCode + " removed from database.");
                            service.Notify("Product " + Product.ProductName + " with code:" + Product.ProductCode + " removed from database.");
                        }
                        using (WarehouseDBEntities context = new WarehouseDBEntities())
                        {
                            ProductList = context.tblProducts.ToList();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Can delete product execute
        /// </summary>
        /// <returns>true or false</returns>
        private bool CanDeleteExecute()
        {
            if (Product == null)
                return false;
            else
                return true;
        }

        private ICommand addCommand;
        /// <summary>
        /// Add command
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(param => AddCommandExecute(), param => CanAddCommandExecute());
                }
                return addCommand;
            }
        }

        /// <summary>
        /// Method for executing add Command, opens view for adding new product
        /// </summary>
        private void AddCommandExecute()
        {
            try
            {
                AddEditProduct addView = new AddEditProduct();
                addView.ShowDialog();
                if((addView.DataContext as AddEditProductViewModel).IsUpdateProduct==true)
                {
                    ProductList = service.GetAllProducts().ToList();  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new product
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddCommandExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to open the edit product window
        /// </summary>
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(param => EditCommandExecute(), param => CanEditCommandExecute());
                }
                return editCommand;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditCommandExecute()
        {
            try
            {
                if (Product != null)
                {
                    AddEditProduct editProduct = new AddEditProduct(Product);
                    editProduct.ShowDialog(); 
                    
                    if((editProduct.DataContext as AddEditProductViewModel).IsUpdateProduct==true)
                    {
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
        /// Checks if the product can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditCommandExecute()
        {
            if (Product == null)
                return false;
            else
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
            managerView.Close();
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
