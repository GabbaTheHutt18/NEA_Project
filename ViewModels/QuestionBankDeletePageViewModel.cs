using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NEA_Project.ViewModels
{
    public class QuestionBankDeletePageViewModel:ObservableObject
    {

        private MainWindowViewModel _parent;
        private ObservableCollection<string> _test = new ObservableCollection<string>();
        private string _selectedValue = "hi";
        public ICommand CheckingButton { get; }
        public QuestionBankDeletePageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            populateList();
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
    }
}
