using System.Collections.Generic;
using Zadatak_1.Services;
using Zadatak_1.Model;

namespace Zadatak_1.Validations
{
    /// <summary>
    /// Class for validating inputs by user
    /// </summary>
    class ValidationClass
    {
        /// <summary>
        /// Checks if the product code is valid
        /// </summary>
        /// <param name="productCode">code of the product</param>
        /// <param name="id">the product id</param>
        /// <returns>null if the input is correct or error message if its wrong</returns>
        public string ProductCodeChecker(string productCode, int id)
        {
            Service service = new Service();
            List<tblProduct> AllProducts = service.GetAllProducts();
            string currentProductCode = "";

            if (productCode == null || productCode.Length < 0 || productCode.Length>10)
            {
                return "Code cannot be empty and can have max 10 characters";
            }

            // Get the current product id
            for (int i = 0; i < AllProducts.Count; i++)
            {
                if (AllProducts[i].ID == id)
                {
                    currentProductCode = AllProducts[i].ProductCode;
                    break;
                }
            }

            // Check if the product already exists, but it is not the current one
            for (int i = 0; i < AllProducts.Count; i++)
            {
                if (AllProducts[i].ProductCode == productCode && currentProductCode != productCode)
                {
                    return "This code already exists! It must be unique";
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if input is integer number
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public string IsNumber(string price)
        {
            if (int.TryParse(price, out int value) == false || value < 0)
            {
                return "Not a valid price";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Cheks if input is zero
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string IsZero(int number)
        {
            if (number <= 0)
            {
                return "Input cannot be zero";
            }
            else
            {
                return null;
            }
        }
    }
}
