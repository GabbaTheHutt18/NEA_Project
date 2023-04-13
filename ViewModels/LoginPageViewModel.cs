using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{

    public class LoginPageViewModel : ObservableObject
    {
        private string _userNameInput = "Enter Your Username Here...";
        private string _passwordInput = "Enter Your Password Here...";
        private MainWindowViewModel _parent;

        public ICommand LoginButtonClickedCommand { get; }
        public LoginPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            LoginButtonClickedCommand = new SimpleCommand(_ => LoginButtonClicked());
        }

        public string UserNameInput
        {
            get => _userNameInput;
            set
            {
                RaiseAndSetIfChanged(ref _userNameInput, value);
            }
        }

        public string PasswordInput
        {
            get => _passwordInput;
            set
            {
                RaiseAndSetIfChanged(ref _passwordInput, value);
            }
        }

        private void LoginButtonClicked()
        {
            
            if (CheckDataBase())
            {
                string UserID = _parent.Database.ReadData("LoginDetails", "UserID", $"UserNames = '{_userNameInput}'", 1)[0];
                _parent.UserID = Int32.Parse(UserID);
                _parent.ChangeToHomePage();
            }
            else
            {

                MessageBoxResult result = MessageBox.Show("Hi friend, seems like you're not on our system, would you like to sign up?", "My App", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    _parent.ChangeToSignUpPage();
                }
                else
                {
                    MessageBox.Show("Oh well, remember that passwords and usernames are case sensitive!", "My App");

                }

            }


        }

        private bool CheckDataBase()
        {
            byte[] hashedPassword = _parent.Hashing(_passwordInput);
            string stringHashedPassword = String.Join(" ", hashedPassword);
            List<string> temp = new List<string>();
            string correctPassword = "";
            try
            {
                correctPassword = _parent.Database.ReadData("LoginDetails", "Passwords", $"UserNames = '{_userNameInput}'", 1)[0];
                //temp = _parent.Database.ReadData("LoginDetails", "Passwords", $"UserNames = '{_userNameInput}'", 1);
                if (temp.Count > 0)
                {
                    
                }
               
            }
            catch (Exception)
            {

                
            }
            
            
            
            if ((stringHashedPassword == correctPassword))
            {
                return true;
            }

            return false; //It worked :)
        }

        


    }

    



}
