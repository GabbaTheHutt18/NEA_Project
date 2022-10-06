using NEA_Project.Constants;
using NEA_Project.Helpers;
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

        public LoginPageViewModel LoginPageViewModel { get; set; }
        public StartPageViewModel StartPageViewModel { get; set; }
        public SignUpPageViewModel SignUpPageViewModel { get; set; }
        

        public ViewStates CurrentPage 
        {
            get => _currentPage;
            set
            { 
                RaiseAndSetIfChanged(ref _currentPage, value);
            }
        }
        public ICommand StartButtonClickedCommand { get; }
        public MainWindowViewModel()

        {
            StartButtonClickedCommand = new SimpleCommand(_ => StartButtonClicked());
            LoginPageViewModel = new LoginPageViewModel();
            StartPageViewModel = new StartPageViewModel(this);
            SignUpPageViewModel = new SignUpPageViewModel();


        }

        public void ChangeToLoginPage()
        {
            CurrentPage = ViewStates.LoginPage;
        }

        private void StartButtonClicked()
        { 
            
        }


    }

    
}
