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
        
        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
        private void RefreshButtonClicked()
        {
            populateList();
        }

        private void populateList()
        {
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID}", 1);
            foreach (string s in hi)
            {
                if (!(_questionBank.Contains(s)))
                { _questionBank.Add(s); }
            }


        }

        public void SelectQuestionBank()
        {
            _questions.Clear();
            _answers.Clear();
            _selectedQuestion = String.Empty;
            MessageBox.Show($"{SelectedQuestionBankName}");
            List<string> question = _parent.Database.ReadData("QuestionBanks", "Question", $"UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}'", 1);
            foreach (string s in question)
            {
                if (!(_questions.Contains(s)))
                { _questions.Add(s); }
            }

        }

        public void ShowAnswer()
        { 
            string hi = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question = '{SelectedQuestion}' AND UserID = {_parent.UserID} AND BankName = '{SelectedQuestionBankName}'", 1)[0];
            MessageBox.Show(hi);
        }
    }
}
