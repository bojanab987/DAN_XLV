using System.ComponentModel;
using Zadatak_1.Validations;

namespace Zadatak_1.Model
{
    public partial class tblProduct : IDataErrorInfo
    {
        ValidationClass validation = new ValidationClass();

        /// <summary>
        /// Propertis for checking
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "ProductCode",
            "Price",
            "Quantity"
        };

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    // there is an error
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Checks if the inputs are incorrect
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Does validations on the property location
        /// </summary>
        /// <param name="propertyName">property we are checking</param>
        /// <returns>if the property is valid (null) or error (string)</returns>
        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "ProductCode":
                        result = this.validation.ProductCodeChecker(ProductCode, ID);
                        break;

                    case "Price":
                        result = this.validation.IsNumber(Price.ToString());
                        break;

                    case "Quantity":
                        result = this.validation.IsZero(Quantity);
                        break;

                    default:
                        result = null;
                        break;
                }

                return result;
            }
        }
    }
}
