using NEA_Project.Commands;
using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class PairsGameViewModel : ObservableObject
    {
        //initialise
        MainWindowViewModel _parent;
        private string _howManyPairs;
        public ICommand CheckPairCommand { get; }
        public ICommand FinishButtonCommand { get; }
        private int _score = 0;
        private string _question = "";
        private string _answer = "";
        private bool _pairFound = false;
        Random random = new Random();
        int randomNum { get; set; }

        private bool _change = false;
        //constructor
        public PairsGameViewModel(MainWindowViewModel Parent) 
        {
            //as the codebehind is tightly coupled, this prevents _parent being overwritten
            if (_parent == null)
            {
                _parent = Parent;
            }
            
            
            CheckPairCommand = new CheckPairCommand(this, _parent);
            FinishButtonCommand = new SimpleCommand(_ => FinishButtonClicked());
        }

        //All these public properties are part of the dependency properties of the code behind
        public MainWindowViewModel ParentVM { get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged(nameof(_parent));
            }
        }

        public string HowManyPairs
        {
            get => _howManyPairs;
            set
            {
                //value = "pls";
                RaiseAndSetIfChanged(ref _howManyPairs, value);
            }
        }

        public bool Change
        {
            get => _change;
            set
            {
                RaiseAndSetIfChanged(ref _change, value);
                if (Change)
                {
                    _question = GetQuestion();
                    _answer = GetAnswer();
                }
            }
        }

        public bool PairFound
        {
            get => _pairFound;
            set
            {
                RaiseAndSetIfChanged(ref _pairFound, value);
            }
        }

        public string Question
        {
            get => _question;



        }

        public string Answer
        {
            get => _answer;


        }


        private string _textBlockContains;
        public string TextBlockContains
        {
            get
            {
                return _textBlockContains;
            }
            set
            {
                RaiseAndSetIfChanged(ref _textBlockContains, value);
            }
        }


        private string _textBlock2Contains;
        public string TextBlock2Contains
        {
            get
            {
                return _textBlock2Contains;
            }
            set
            {
                RaiseAndSetIfChanged(ref _textBlock2Contains, value);

            }
        }


        public int Score
        {
            get => _score;
            set { RaiseAndSetIfChanged(ref _score, value); ; }
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
            randomNum = random.Next(1, _parent.Database.GetSize("QuestionBanks", "QuestionID", $"WHERE BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}") + 1);
            
            return _parent.Database.ReadData("QuestionBanks", "Question", $"QuestionID = {randomNum} AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}",1)[0];
        }

        //Returns the answer for the current question first assigns ID depending on whether 
        // the current question bank is the "Default" bank or not. Then, it queries the database to retrieve the answer
        public string GetAnswer()
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
            
            return _parent.Database.ReadData("QuestionBanks", "Answer", $"Question LIKE '{_question}' AND QuestionID = {randomNum} AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}",1)[0];
        }


        //When the finish button is clicked, the highscore database is read and the existing score is saved
        //this is then compared to the new score, if the new score is greater, it replaces the old score and
        // the page is changed to the Game Menu. 
        private void FinishButtonClicked()
        {
            int NumberOfScores = _parent.Database.GetSize("PairsScores", "UserID", $"WHERE UserID = {_parent.UserID}");
            NumberOfScores += 1;
            _parent.Database.InsertData("PairsScores", "PairsID, UserID, Score", $"{NumberOfScores},{_parent.UserID},'{Score}'");
            Score = 0;
            _parent.ChangeToGameMenuPage();

            
        }

    }
}
