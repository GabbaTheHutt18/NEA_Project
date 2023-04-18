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
    public class QuestionBankReadViewModel : ObservableObject
    {
        //Initialise
        MainWindowViewModel _parent;
        private ObservableCollection<string> _questionBank = new ObservableCollection<string>();
        private ObservableCollection<string> _questions = new ObservableCollection<string>();
        private ObservableCollection<string> _answers = new ObservableCollection<string>();
        private List<string> _test = new List<string>();

        private string _selectedQuestionBankName = "";
        private string _selectedQuestion = "";
        
        public ICommand SelectQuestionBankCommand { get; }
        public ICommand MenuButtonClickedCommand { get; }
        public ICommand RefreshButtonClickedCommand { get; }
        public ICommand ShowAnswerCommand { get; }

        //Constructor
        public QuestionBankReadViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            MenuButtonClickedCommand = new SimpleCommand(_ => MenuButtonClicked());
            RefreshButtonClickedCommand = new SimpleCommand(_ => RefreshButtonClicked()); 
            SelectQuestionBankCommand = new SimpleCommand(_ => SelectQuestionBank());
            ShowAnswerCommand = new SimpleCommand(_ => ShowAnswer());
        }

        public ObservableCollection<string> QuestionBankNames { get => _questionBank; }
        public ObservableCollection<string> Questions { get => _questions; set { RaiseAndSetIfChanged(ref _questions, value); } }
        public ObservableCollection<string> Answers { get => _answers; set { RaiseAndSetIfChanged(ref _answers, value); } }

        public string SelectedQuestionBankName { get => _selectedQuestionBankName; set { _selectedQuestionBankName = value; } }
        public string SelectedQuestion { get => _selectedQuestion; set { RaiseAndSetIfChanged(ref _selectedQuestion, value); } }

        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
        //when button is pressed, method populateList is called 
        private void RefreshButtonClicked()
        {
            populateList();
        }

        //In order to populate the ComboBox, there must be a list of all the possible countries saved, this reads the database
        //and saves all the names to the list. 
        private void populateList()
        {
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID} OR UserID = 0", 1);
            foreach (string s in hi)
            {
                if (!(_questionBank.Contains(s)))
                { _questionBank.Add(s); }
            }


        }

        //this method selects a question bank based on the user input, then reads the questions from the
        //selected bank and adds them to a list of questions to populate the question combobox. 
        public void SelectQuestionBank()
        {
            _questions.Clear();
            _answers.Clear();
            _selectedQuestion = String.Empty;
            MessageBox.Show($"{SelectedQuestionBankName}");
            int ID;
            if (SelectedQuestionBankName == "Default")
            {
                ID = 0;
            }
            else
            {
                ID = _parent.UserID;
            }
            List<string> question = _parent.Database.ReadData("QuestionBanks", "Question", $"BankName = '{SelectedQuestionBankName}' AND USERID = {ID}", 1);
            foreach (string s in question)
            {
                if (!(_questions.Contains(s)))
                { _questions.Add(s); }
            }

        }
        //reads the database and displays a message box of the answer 
        public void ShowAnswer()
        {
            int ID;
            if (SelectedQuestionBankName == "Default")
            {
                ID = 0;
            }
            else
            {
                ID = _parent.UserID;
            }
            string answer = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question = '{SelectedQuestion}' AND BankName = '{SelectedQuestionBankName}' AND USERID = {ID}", 1)[0];
            MessageBox.Show(answer);
        }
    }
}
