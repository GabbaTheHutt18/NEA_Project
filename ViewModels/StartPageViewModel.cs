using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class StartPageViewModel : ObservableObject
    {
        //Initialise
        private MainWindowViewModel _parent;
       
        public ICommand LoginButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }

        //Constructor
        public StartPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            LoginButtonClickedCommand = new SimpleCommand(_ => LoginButtonClicked());
            SignUpButtonClickedCommand = new SimpleCommand(_ => SignUpButtonClicked());
            
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void LoginButtonClicked()
        {

            _parent.ChangeToLoginPage();
           
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void SignUpButtonClicked()
        {
            _parent.ChangeToSignUpPage();
            
        }

        
    }
}
