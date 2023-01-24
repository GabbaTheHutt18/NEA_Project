using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
                _parent.ChangeToHomePage();
            }
                
        }

        private bool CheckDataBase()
        {
           
            string correctPassWord =_parent.LoginDataBase.ReadData("LoginDetails", "Passwords", $"Usernames = '{_userNameInput}'");
            if (correctPassWord == _passwordInput)
            {
                return true;
            }
            return false;
        }

        


    }

    



}
