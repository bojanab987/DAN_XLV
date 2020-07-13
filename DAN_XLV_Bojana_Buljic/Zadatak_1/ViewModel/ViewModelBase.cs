using System.ComponentModel;

namespace Zadatak_1.ViewModel
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when some property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Register each change on control/command binded for a Property
        /// </summary>
        /// <param name="propertyName">string parameter for a property name</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
