using System.Windows;

namespace Zadatak_1.Services
{
    /// <summary>
    /// Class for displaying message to user
    /// </summary>
    class ShowMessage
    {
        /// <summary>
        /// Notify user if product is stored
        /// </summary>
        /// <param name="message">Message informing about stored product</param>
        public void ShowMessageToUser(string message)
        {
            MessageBox.Show(message);
        }
    }
}
