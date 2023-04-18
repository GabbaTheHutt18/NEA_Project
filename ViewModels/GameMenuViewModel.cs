using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class GameMenuViewModel:ObservableObject
    {
        //Initialise 
        MainWindowViewModel _parent;
        public ICommand HomeButtonClickedCommand { get; }
        public ICommand PairsGameButtonClickedCommand { get; }
        public ICommand WordScrambleButtonClickedCommand { get; }
        public ICommand QuizButtonClickedCommand { get; }
        public ICommand RefreshButtonClickedCommand { get; }

        private ObservableCollection<string> _questionBanks = new ObservableCollection<string>();
        
        //Constructor 
        public GameMenuViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
            PairsGameButtonClickedCommand = new SimpleCommand(_ => PairsGameButtonClicked());
            WordScrambleButtonClickedCommand = new SimpleCommand(_ => WordScrambleButtonClicked());
            QuizButtonClickedCommand = new SimpleCommand(_ => QuizButtonClicked());
            RefreshButtonClickedCommand = new SimpleCommand(_ => RefreshButtonClicked());
        }
        public ObservableCollection<string> QuestionBanks { get => _questionBanks; set { RaiseAndSetIfChanged(ref _questionBanks, value); } }

        public string selectedValue { get => _parent.CurrentQuestionBank; 
            set { _parent.CurrentQuestionBank = value; } }

        //In order to populate the ComboBox, there must be a list of all the question banks saved, this reads the database
        //and saves all the names to the list. 
        private void populateList()
        {
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID} OR UserID = 0", 1);
            foreach (string s in hi)
            {
                if (!(_questionBanks.Contains(s)))
                { _questionBanks.Add(s); }
            }

        }
        //when button is clicked, populateList is called. 
        private void RefreshButtonClicked()
        {
            populateList();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void HomeButtonClicked()
        { 
            _parent.ChangeToHomePage();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void PairsGameButtonClicked()
        { 
            _parent.ChangeToPairsGamePage();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void WordScrambleButtonClicked()
        {
            _parent.ChangeToWordScramblePage();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void QuizButtonClicked()
        { 
            _parent.ChangeToQuizPage();
        }
    }
}
