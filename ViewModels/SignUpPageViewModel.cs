using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class SignUpPageViewModel : ObservableObject
    {
        private string _userNameInput = "Enter Your Username Here...";
        private string _passwordInput = "Enter Your Password Here...";
        private MainWindowViewModel _parent;
      

        public ICommand VerifyButtonClickedCommand { get; }
        public SignUpPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            VerifyButtonClickedCommand = new SimpleCommand(_ => VerifyButtonClicked());
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

        private void VerifyButtonClicked()
        {
            if (_userNameInput != null && _passwordInput.Length > 4) {
                if (CheckDatabase())
                {
                    MessageBoxResult result = MessageBox.Show("Hi friend, seems this username already exists, would you like to login?", "My App", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        _parent.ChangeToLoginPage();
                    }
                    else
                    {
                        MessageBox.Show("Oh well, remember that usernames are case sensitive and should be unique to you!", "My App");

                    }
                }
                else
                {
                    AddToDatabase();
                }
            }
            
           
        }

        private bool CheckDatabase()
        {
            try
            {
                if (_parent.Database.ReadData("LoginDetails", "UserNames", $"Usernames = '{_userNameInput}'", 1)[0] == _userNameInput)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                
            }
            return false;

        }

        private void AddToDatabase()
        {
            int DataBaseSize = 0;

            try
            {
                DataBaseSize = _parent.Database.GetSize("LoginDetails", "UserID", "");
            }
            catch (Exception)
            {


            }
            DataBaseSize += 1;
            byte[] hashedPassword = _parent.Hashing(_passwordInput);
            string stringHashedPassword = String.Join(" ", hashedPassword);
            _parent.Database.InsertData("LoginDetails", "UserID, UserNames, Passwords", $"{DataBaseSize},'{_userNameInput}', '{stringHashedPassword}'");
            _parent.Database.InsertData("UserStats","UserID, HighScore1,HighScore2,HighScore3", $"{DataBaseSize}, 0, 0, 0");
            _parent.ChangeToHomePage();

        }



    }
}
