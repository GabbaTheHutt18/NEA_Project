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
        MainWindowViewModel _parent;
        private string _howManyPairs;
        public ICommand CheckPairCommand { get; }
        private int _score = 0;
        private string _question = "pls";
        private string _answer = "hi";
        private bool _pairFound = false;
        Random random = new Random();
        public List<string[]> test = new List<string[]>();
        int randomNum { get; set; }

        private bool _change = false;
        public PairsGameViewModel(MainWindowViewModel Parent) 
        {
            if (_parent == null)
            {
                _parent = Parent;
            }
            
            
            CheckPairCommand = new CheckPairCommand(this, _parent);
        }

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
                _textBlockContains = value;
                OnPropertyChanged(nameof(TextBlockContains));
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


    }
}
