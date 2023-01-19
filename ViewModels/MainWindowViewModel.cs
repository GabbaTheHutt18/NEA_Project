using NEA_Project.Constants;
using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private ViewStates _currentPage = ViewStates.StartPage;
        public Database LoginDataBase = new Database("LoginDetails", "UserNames VARCHAR(20), Passwords VARCHAR(20)");
        public LoginPageViewModel LoginPageViewModel { get; set; }
        public StartPageViewModel StartPageViewModel { get; set; }
        public SignUpPageViewModel SignUpPageViewModel { get; set; }
        public HomePageViewModel HomePageViewModel { get; set; }    
        

        public ViewStates CurrentPage 
        {
            get => _currentPage;
            set
            { 
                RaiseAndSetIfChanged(ref _currentPage, value);
            }
        }
        public MainWindowViewModel()

        {
            LoginPageViewModel = new LoginPageViewModel(this);
            StartPageViewModel = new StartPageViewModel(this);
            SignUpPageViewModel = new SignUpPageViewModel(this);
            HomePageViewModel = new HomePageViewModel(this);
       


        }

        public void ChangeToLoginPage()
        {
            CurrentPage = ViewStates.LoginPage;
        }

        public void ChangeToSignUpPage()
        {
            CurrentPage = ViewStates.SignUpPage;
        }

        public void ChangeToHomePage()
        {
            CurrentPage = ViewStates.HomePage;
        }

        
    }

    
}
