﻿using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
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

            _parent.LoginDataBase.InsertData("LoginDetails","UserNames, Passwords", $"'{_userNameInput}', '{_passwordInput}'");
            _parent.ChangeToHomePage();



        }



    }
}
