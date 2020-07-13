using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class LogInViewModel:ViewModelBase
    {
        LogInView logInView;

        #region Constructor
        public LogInViewModel(LogInView logInOpen)
        {
            logInView = logInOpen;           
        }
        #endregion

        #region Properties   
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// LogIn Command
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {                    
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Method for deciding which View will open according to logged in Employee credentials
        /// </summary>
        private void LoginExecute(object o)
        {
            try
            {
                string password = (o as PasswordBox).Password;
                if (Username == "Mag2019" && password == "Mag2019")
                {
                    EmployeeView employee = new EmployeeView();
                    logInView.Close();
                    employee.ShowDialog();
                }
                else if (Username == "Man2019" && password == "Man2019")
                {
                    ManagerView manager = new ManagerView();
                    logInView.Close();
                    manager.ShowDialog();
                    return;
                }
                else
                {
                    MessageBox.Show("Username or password not correct.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       
        #endregion
    }
}
