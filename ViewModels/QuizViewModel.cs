using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class QuizViewModel : ObservableObject
    {
        MainWindowViewModel _parent;
        private string _question = "";
        private string _userInput = "";
        private int _score = 0;
        public Random random = new Random();

        public ICommand VerifyButtonClickedCommand { get; }
        public ICommand CheckAnswerCommand { get; }
        public ICommand FinishButtonCommand { get; }
        public QuizViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            VerifyButtonClickedCommand = new SimpleCommand(_ => GenerateQuestionButtonClicked());
            CheckAnswerCommand = new SimpleCommand(_ => CheckAnswer());
            FinishButtonCommand = new SimpleCommand(_ => FinishButtonClicked());
        }
        public string Question
        {
            get => _question;
            set
            {
                RaiseAndSetIfChanged(ref _question, value);
            }
        }

        public string UserInput
        {
            get => _userInput;
            set
            {
                //value = "pls";
                RaiseAndSetIfChanged(ref _userInput, value);
            }
        }

        public int Score
        {
            get => _score;
            set { RaiseAndSetIfChanged(ref _score, value); }
        }


        private void GenerateQuestionButtonClicked()
        {

            Question = GetQuestion();

        }

        private void FinishButtonClicked()
        {
            int existingScore = 0;
            try
            {
                string DatabaseScore = _parent.Database.ReadData("UserStats", "HighScore1", $"USERID = {_parent.UserID}",1)[0];
                existingScore = Int32.Parse(DatabaseScore);
            }
            catch (Exception)
            {

                
            }
            if (existingScore < Score)
            {
                _parent.Database.UpdateData("UserStats", $"HighScore1 = {Score}", $"USERID = {_parent.UserID}");
            }
            Score = 0;
            _parent.ChangeToGameMenuPage();
        }


        public void CheckAnswer()
        {

            string answer = UserInput.Trim();
            string CorrectAnswer = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question LIKE '{Question}' AND BankName = '{_parent.CurrentQuestionBank}' AND (UserID = {_parent.UserID} OR UserID = 0)",1)[0];

            if (answer == CorrectAnswer.Trim())
            {
                MessageBox.Show("yay!");
                Score += 1;
            }
            else
            {
                MessageBox.Show($"neigh </3 {CorrectAnswer}");
                Score -= 1;
            }
            GetQuestion();
        }

        public string GetQuestion()
        {
            int ID;
            if (_parent.CurrentQuestionBank == "Default")
            {
                ID = 0;
            }
            else
            {
                ID = _parent.UserID;
            }
            int question = random.Next(1, _parent.Database.GetSize("QuestionBanks", "QuestionID", $"WHERE BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}") + 1);
            return _parent.Database.ReadData("QuestionBanks", "Question", $"QuestionID = {question} AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}", 1)[0];
            //OUT OF RANGE ERROR, IDs Don't Start at 0 AND NEXT Max value is exclusive!!!
        }
    }
}
