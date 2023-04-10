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
        MainWindowViewModel _parent;
        public ICommand HomeButtonClickedCommand { get; }
        public ICommand PairsGameButtonClickedCommand { get; }
        public ICommand WordScrambleButtonClickedCommand { get; }
        public ICommand QuizButtonClickedCommand { get; }
        public ICommand RefreshButtonClickedCommand { get; }

        private ObservableCollection<string> _test = new ObservableCollection<string>();
        //private string _selectedValue = "hi";

        public GameMenuViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
            PairsGameButtonClickedCommand = new SimpleCommand(_ => PairsGameButtonClicked());
            WordScrambleButtonClickedCommand = new SimpleCommand(_ => WordScrambleButtonClicked());
            QuizButtonClickedCommand = new SimpleCommand(_ => QuizButtonClicked());
            RefreshButtonClickedCommand = new SimpleCommand(_ => RefreshButtonClicked());
        }
        public ObservableCollection<string> testing { get => _test; set { RaiseAndSetIfChanged(ref _test, value); } }

        public string selectedValue { get => _parent.CurrentQuestionBank; 
            set { _parent.CurrentQuestionBank = value; } }
        private void populateList()
        {
            //int size = _parent.Database.GetSize("QuestionBanks", "UserID", "");
            List<string> hi = _parent.Database.ReadData("QuestionBanks", "BankName", $"USERID = {_parent.UserID} OR UserID = 0", 1);
            foreach (string s in hi)
            {
                if (!(_test.Contains(s)))
                { _test.Add(s); }
            }

        }
        private void RefreshButtonClicked()
        {
            populateList();
        }
        private void HomeButtonClicked()
        { 
            _parent.ChangeToHomePage();
        }
        private void PairsGameButtonClicked()
        { 
            _parent.ChangeToPairsGamePage();
        }
        private void WordScrambleButtonClicked()
        {
            _parent.ChangeToWordScramblePage();
        }
        private void QuizButtonClicked()
        { 
            _parent.ChangeToQuizPage();
        }
    }
}
