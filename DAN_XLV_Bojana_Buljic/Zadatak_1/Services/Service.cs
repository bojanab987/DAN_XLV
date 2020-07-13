using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Services
{
    class Service
    {
        public Service()
        {
            //events to notify
            OnNotification = logger.WriteToFile;
            OnNotificationEmp = messageTouser.ShowMessageToUser;
        }
        Logger logger = new Logger();
        ShowMessage messageTouser = new ShowMessage();

        #region EventandDelegate logger
        /// <summary>
        /// Delegate for sending notifications depending on the parameter value.
        /// </summary>
        /// <param name="text">text that is being printed into the file</param>
        public delegate void Notification(string text);        
        /// <summary>
        /// Event that gets triggered when a text is given
        /// </summary>
        public event Notification OnNotification;

        /// <summary>
        /// Checks if there is any given value to trigger the event
        /// </summary>
        /// <param name="text">Parameter given to notify</param>
        internal void Notify(string text)
        {
            if (OnNotification != null)
            {
                OnNotification(text);
            }
        }
        #endregion

        #region EventandDelegate ShowMessage
        /// <summary>
        /// Delegate for sending notification to user
        /// </summary>
        /// <param name="text"></param>
        public delegate void NotificationEmp(string text);

        /// <summary>
        /// event for informing user of change
        /// </summary>
        public event NotificationEmp OnNotificationEmp;

        /// <summary>
        /// Raise an event for notify user about storing products
        /// </summary>
        /// <param name="text"></param>
        internal void NotifyEmployee(string text)
        {
            if (OnNotificationEmp != null)
            {
                OnNotificationEmp(text);
            }
        }
        #endregion

        /// <summary>
        /// check which action is taken
        /// </summary>
        public static string action;

        #region Methods
        /// <summary>
        /// Method gets all products from database
        /// </summary>
        /// <returns>List of products</returns>
        public List<tblProduct> GetAllProducts()
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    List<tblProduct> products= new List<tblProduct>();
                    products = (from x in context.tblProducts select x).ToList();
                    OnNotification = logger.WriteToFile;
                    return products;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }       
        
        /// <summary>
        /// Method delets product from database if product is not stored
        /// </summary>
        /// <param name="productID">product id</param>
        public void DeleteProduct(int productID)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    tblProduct productToDelete = (from p in context.tblProducts where p.ID == productID select p).First();
                    if (productToDelete.Stored =="no")
                    {
                        context.tblProducts.Remove(productToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Stored products cannot be deleted.");
                    }                                      
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Method for add or edit products in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>new or edited product</returns>
        public tblProduct AddEditProduct(tblProduct product)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    if(product.ID==0)
                    {
                        tblProduct newProduct = new tblProduct();
                        newProduct.ProductName = product.ProductName;
                        newProduct.ProductCode = product.ProductCode;
                        newProduct.Quantity = product.Quantity;
                        newProduct.Price = product.Price;
                        newProduct.Stored = "no";
                        context.tblProducts.Add(newProduct);
                        context.SaveChanges();
                        product.ID = newProduct.ID;
                        action = "added";
                        return product;
                    }
                    else
                    {
                        tblProduct productForEdit = (from x in context.tblProducts where x.ID == product.ID select x).FirstOrDefault();
                        productForEdit.ProductName = product.ProductName;
                        productForEdit.ProductCode = product.ProductCode;
                        productForEdit.Quantity = product.Quantity;
                        productForEdit.Price = product.Price;
                        productForEdit.Stored = product.Stored;
                        context.SaveChanges();
                        action = "edited";
                        return product;
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }  
        
        /// <summary>
        /// Method to get all stored products from table
        /// </summary>
        /// <returns>list of stored products</returns>
        public List<tblProduct> GetStoredProducts()
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    List<tblProduct> stored = new List<tblProduct>();
                    stored = (from x in context.tblProducts where x.Stored == "yes" select x).ToList();
                    return stored;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for change of product Stored status
        /// </summary>
        /// <param name="product">product for storing</param>
        public void StoreProduct(tblProduct product)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    tblProduct productToStore = (from x in context.tblProducts where x.ID == product.ID select x).First();
                    List<tblProduct> storedList = GetStoredProducts();
                    int counter = 0;
                    foreach (var item in storedList)
                    {
                        counter += item.Quantity;
                    }

                    //check quantity if its less than 100 it can be stored
                    if(productToStore.Stored=="no" && (counter+productToStore.Quantity<=100))
                    {
                        //change status to yes
                        productToStore.Stored = "yes";
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }

        }
        #endregion
    }
}
