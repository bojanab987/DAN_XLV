using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zadatak_1.Services
{
    class Service
    {
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
        

        //store product

    }
}
