using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class QuestionBankDeleteViewModel : ObservableObject
    {
        MainWindowViewModel _parent;
        private ObservableCollection<string> _test = new ObservableCollection<string>();
        private string _selectedValue = "hi";
        public ICommand CheckingButton { get; }
        public ICommand MenuButtonClickedCommand { get; }
        public QuestionBankDeleteViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            populateList();
            MenuButtonClickedCommand = new SimpleCommand(_ => MenuButtonClicked());
            CheckingButton = new SimpleCommand(_ => DeleteButton());
        }
        public ObservableCollection<string> testing { get => _test; set { RaiseAndSetIfChanged(ref _test, value); } }

        public string selectedValue { get => _selectedValue; set { _selectedValue = value; } }

        private void DeleteButton()
        {
            //MessageBox.Show($"yay {_selectedValue}  !!");
            if (MessageBox.Show($"Delete {_selectedValue} question bank?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //do no stuff
                _parent.Database.DeleteData("QuestionBanks", $"BankName = '{selectedValue}'");
                _test.Remove(_selectedValue);
                MessageBox.Show("Successfully Deleted!");
            }


        }
        private void populateList()
        {
            //int size = _parent.Database.GetSize("QuestionBanks", "UserID", "");
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID}",1);
            foreach (string s in hi)
            {
                if (!(_test.Contains(s)))
                { _test.Add(s); }
            }

        }
        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
    }
}
