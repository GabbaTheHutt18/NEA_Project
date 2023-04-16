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
        private ObservableCollection<string> _questionBanks = new ObservableCollection<string>();
        private string _selectedValue = "";
        public ICommand CheckingButton { get; }
        public ICommand MenuButtonClickedCommand { get; }
        public ICommand RefreshButtonClickedCommand { get; }
        public QuestionBankDeleteViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            PopulateList();
            MenuButtonClickedCommand = new SimpleCommand(_ => MenuButtonClicked());
            CheckingButton = new SimpleCommand(_ => DeleteButton());
            RefreshButtonClickedCommand = new SimpleCommand(_ => RefreshButtonClicked());
        }
        public ObservableCollection<string> QuestionBanks { get => _questionBanks; set { RaiseAndSetIfChanged(ref _questionBanks, value); } }

        public string selectedValue { get => _selectedValue; set { _selectedValue = value; } }

        private void DeleteButton()
        {
            //MessageBox.Show($"yay {_selectedValue}  !!");
            if (MessageBox.Show($"Delete {_selectedValue} question bank?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //do no stuff
                _parent.Database.DeleteData("QuestionBanks", $"BankName = '{selectedValue}'");
                _questionBanks.Remove(_selectedValue);
                MessageBox.Show("Successfully Deleted!");
            }


        }
        public void RefreshButtonClicked()
        {
            PopulateList();
        }
        private void PopulateList()
        {
            
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID}",1);
            foreach (string s in hi)
            {
                if (!(_questionBanks.Contains(s)))
                { _questionBanks.Add(s); }
            }

        }
        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
    }
}
