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
        private MainWindowViewModel _parent;
        private string _debugText = "debug";

        public ICommand LoginButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }

        public StartPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            LoginButtonClickedCommand = new SimpleCommand(_ => LoginButtonClicked());
            SignUpButtonClickedCommand = new SimpleCommand(_ => SignUpButtonClicked());
            DebugText = _parent.CurrentPage.ToString();
        }

        public string DebugText
        {
            get => _debugText;
            set 
            {
                RaiseAndSetIfChanged(ref _debugText, value);
            }
        }

        private void LoginButtonClicked()
        {

            _parent.ChangeToLoginPage();
           
        }

        private void SignUpButtonClicked()
        {
            _parent.ChangeToSignUpPage();
            
        }
    }
}
