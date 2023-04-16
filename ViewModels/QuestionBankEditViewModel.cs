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
    public class QuestionBankEditViewModel : ObservableObject
    {
        MainWindowViewModel _parent;
        private ObservableCollection<string> _questionBank = new ObservableCollection<string>();
        private ObservableCollection<string> _questions = new ObservableCollection<string>();
        private ObservableCollection<string> _answers = new ObservableCollection<string>();
        private string _selectedQuestionBankName = "";
        private string _selectedQuestion = "";
        private string _selectedAnswer = "";
        private string _updatedQuestion = "";
        private string _updatedAnswer = "";
        public ICommand SelectQuestionBankCommand { get; }
        public ICommand UpdateQuestionCommand { get; }
        public ICommand UpdateAnswerCommand { get; }
        public ICommand SelectQuestionCommand { get; }
        public ICommand MenuButtonClickedCommand { get; }
        public ICommand RefreshButtonClickedCommand { get; }
        public QuestionBankEditViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            populateList();
            MenuButtonClickedCommand = new SimpleCommand(_ => MenuButtonClicked());
            SelectQuestionBankCommand = new SimpleCommand(_ => SelectQuestionBank());
            UpdateQuestionCommand = new SimpleCommand(_ => UpdateQuestion());
            UpdateAnswerCommand = new SimpleCommand(_ => UpdateAnswer());
            SelectQuestionCommand = new SimpleCommand(_ => SelectQuestion());
            RefreshButtonClickedCommand = new SimpleCommand(_ => RefreshButtonClicked());
        }

        public ObservableCollection<string> QuestionBankNames { get => _questionBank; }
        public ObservableCollection<string> Questions { get => _questions; set { RaiseAndSetIfChanged(ref _questions, value); } }
        public ObservableCollection<string> Answers { get => _answers; set { RaiseAndSetIfChanged(ref _answers, value); } }

        public string SelectedQuestionBankName { get => _selectedQuestionBankName; set { _selectedQuestionBankName = value; } }
        public string SelectedQuestion { get => _selectedQuestion; set { RaiseAndSetIfChanged(ref _selectedQuestion, value); } }
        public string SelectedAnswer { get => _selectedAnswer; set { RaiseAndSetIfChanged(ref _selectedAnswer, value); } }
        public string UpdatedQuestion { get => _updatedQuestion; set { _updatedQuestion = value; } }
        public string UpdatedAnswer { get => _updatedAnswer; set { _updatedAnswer = value; } }


        private void populateList()
        {
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID}", 1);
            foreach (string s in hi)
            {
                if (!(_questionBank.Contains(s)))
                { _questionBank.Add(s); }
            }


        }

        private void RefreshButtonClicked()
        {
            populateList();
        }

        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }

        public void SelectQuestionBank()
        {
            _questions.Clear();
            _answers.Clear();
            _selectedAnswer = String.Empty;
            _selectedQuestion = String.Empty;
            MessageBox.Show($"{SelectedQuestionBankName}");
            List<string> question = _parent.Database.ReadData("QuestionBanks", "Question", $"UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}'",1);
            foreach (string s in question)
            {
                if (!(_questions.Contains(s)))
                { _questions.Add(s); }
            }



        }

        public void SelectQuestion()
        {
            List<string> answer = _parent.Database.ReadData("QuestionBanks", "Answer", $"UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}' AND Question = '{SelectedQuestion}' ", 1);
            foreach (string s in answer)
            {
                if (!(_answers.Contains(s)))
                { _answers.Add(s); }
            }
        }


        public void UpdateQuestion()
        {
            if (SelectedQuestionBankName == String.Empty)
            {
                MessageBox.Show("No bank selected!");

            }
            else if (SelectedQuestion == String.Empty)
            {
                MessageBox.Show("Oh no! Please select your Question!");
            }
            else if (UpdatedQuestion == String.Empty)
            {
                MessageBox.Show("Oh no! Please enter your new Question!");
            }
            else
            {
                string ID = _parent.Database.ReadData("QuestionBanks", "QuestionID", $"Question = '{SelectedQuestion}' AND UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}'",1)[0];
                int QuestionID = Int32.Parse(ID);
                

                _parent.Database.UpdateData("QuestionBanks", $"Question = '{UpdatedQuestion}'", $"UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}' AND QuestionID = {QuestionID}");
            }
            UpdatedQuestion = String.Empty;

        }
        public void UpdateAnswer()
        {
            if (SelectedQuestionBankName == String.Empty)
            {
                MessageBox.Show("No bank selected!");

            }
            else if (SelectedAnswer == String.Empty)
            {
                MessageBox.Show("Oh no! Please select your Answer!");
            }
            else if (UpdatedAnswer == String.Empty)
            {
                MessageBox.Show("Oh no! Please enter your new Answer!");
            }
            else
            {
                // MISSING THE QUESTION ID CONDITION. 
                string ID = _parent.Database.ReadData("QuestionBanks", "QuestionID", $"Question = '{SelectedQuestion}' AND UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}'", 1)[0];
                int QuestionID = Int32.Parse(ID);
                _parent.Database.UpdateData("QuestionBanks", $"Answer = '{UpdatedAnswer}'", $"UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}' AND QuestionID = {QuestionID}");
            }
            UpdatedAnswer = String.Empty;

        }

    }
}
