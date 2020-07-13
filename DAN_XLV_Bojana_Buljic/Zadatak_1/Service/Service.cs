using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Service
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
        //add product
        //delete product
        //update product

        //store product

    }
}
