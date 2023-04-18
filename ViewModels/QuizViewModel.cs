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
        //Initialise
        MainWindowViewModel _parent;
        private string _question = "";
        private string _userInput = "";
        private int _score = 0;
        public Random random = new Random();

        public ICommand VerifyButtonClickedCommand { get; }
        public ICommand CheckAnswerCommand { get; }
        public ICommand FinishButtonCommand { get; }
        //Constructor
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

        //When the finish button is clicked, the highscore database is read and the existing score is saved
        //this is then compared to the new score, if the new score is greater, it replaces the old score and
        // the page is changed to the Game Menu. 
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

        //when the check answer button is pressed, the user's input is compared with the actual answer
        //,the score is adjusted as fit and a message is displayed
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

        //when generate question button is clicked, the method first distinguishes which question bank they are using, if its the default 
        // the ID needs to change because of the way it is saved in the database
        //then a random question is read from the database. 
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
            
        }
    }
}
