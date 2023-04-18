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
        //initialise
        MainWindowViewModel _parent;
        private ObservableCollection<string> _questionBanks = new ObservableCollection<string>();
        private string _selectedValue = "";
        public ICommand CheckingButton { get; }
        public ICommand MenuButtonClickedCommand { get; }
        public ICommand RefreshButtonClickedCommand { get; }
        //constructor
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

        //method used to delete a question bank dependent on user's input and confirms the decision
        private void DeleteButton()
        {
            
            if (MessageBox.Show($"Delete {_selectedValue} question bank?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //confirms the users choice
                _parent.Database.DeleteData("QuestionBanks", $"BankName = '{selectedValue}'");
                _questionBanks.Remove(_selectedValue);
                MessageBox.Show("Successfully Deleted!");
            }


        }
        //when pressed, this button calls the method populateList
        public void RefreshButtonClicked()
        {
            PopulateList();
        }

        //In order to populate the ComboBox, there must be a list of all the possible countries saved, this reads the database
        //and saves all the names to the list. 
        private void PopulateList()
        {
            
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID}",1);
            foreach (string s in hi)
            {
                if (!(_questionBanks.Contains(s)))
                { _questionBanks.Add(s); }
            }

        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
    }
}
