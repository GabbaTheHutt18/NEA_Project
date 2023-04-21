using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class UserStatsViewModel : ObservableObject
    {
        //Initialise
        MainWindowViewModel _parent;
        private double _userPercentile;
        private int _quizScoreAverage = 0;
        private int _pairsScoreAverage = 0;
        private int _wordScrambleAverage = 0;
        public MergeSort MergeSort;
        private string _quizHighScore;
        private string _pairsHighScore;
        private string _wordScrambleHighScore;
        private List<string> _highScoreCategories = new List<string>() 
        { "Quiz HighScore", "Pairs HighScore", "Word Scramble HighScore"};
        private string _selectedHighScore;
        public ICommand GetUserStatsCommand { get; }
        public ICommand GetPercentagesCommand { get; }
        public ICommand HomeButtonClickedCommand { get; }
        public ICommand GetAverageCommand { get;  }
        //Constructor
        public UserStatsViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            MergeSort = new MergeSort();
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
            GetUserStatsCommand = new SimpleCommand(_=> GetUserStats());
            GetPercentagesCommand = new SimpleCommand(_ => GetPercentile());
            GetAverageCommand = new SimpleCommand(_ => GetAverageScore());
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void HomeButtonClicked()
        {
            _parent.ChangeToHomePage();
        }
        public string QuizHighScore { get => _quizHighScore; set { RaiseAndSetIfChanged(ref _quizHighScore, value); } }
        public string PairsHighScore { get => _pairsHighScore; set { RaiseAndSetIfChanged(ref _pairsHighScore, value); } }
        public string WordHighScore { get => _wordScrambleHighScore; set { RaiseAndSetIfChanged(ref _wordScrambleHighScore, value); } }
        public int QuizScoreAverage { get => _quizScoreAverage; set { RaiseAndSetIfChanged(ref _quizScoreAverage, value); } }
        public int PairsScoreAverage { get => _pairsScoreAverage; set { RaiseAndSetIfChanged(ref _pairsScoreAverage, value); } }
        public int WordScrambleScoreAverage { get => _wordScrambleAverage; set { RaiseAndSetIfChanged(ref _wordScrambleAverage, value); } }

        public List<string> HighScoresCategories { get => _highScoreCategories; }

        public string selectedValue
        {
            get => _selectedHighScore;
            set { _selectedHighScore = value; }
        }

        public void GetUserStats()
        {
            //Reads database and assigns values
            List<string> QuizScores = _parent.Database.ReadData("QuizScores", "MAX(Score)", "USERID = 1", 1);
            List<string> PairsScores = _parent.Database.ReadData("PairsScores", "MAX(Score)", "USERID = 1", 1);
            List<string> WordScrambleScores = _parent.Database.ReadData("WordScrambleScores", "MAX(Score)", "USERID = 1", 1);
            if (QuizScores.Count != 0)
            {
                QuizHighScore = QuizScores[0];
            }
            else
            {
                QuizHighScore = "0";
            }
            if (PairsScores.Count != 0)
            {
                PairsHighScore = PairsScores[0];
            }
            else
            {
                PairsHighScore = "0";
            }
            if (WordScrambleScores.Count != 0)
            {
                WordHighScore = WordScrambleScores[0];
            }
            else
            {
                WordHighScore = "0";
            }
            
            
            
        }
        
        

        //populates a list of high scores for each user in a database table 
        
        private void GetAverageScore()
        {
            int NumberOfQuizScores = _parent.Database.GetSize("QuizScores", "QuizID", "WHERE UserID = 1");
            int NumberOfPairsScores = _parent.Database.GetSize("PairsScores", "PairsID", "WHERE UserID = 1");
            int NumberOfWordScrambleScores = _parent.Database.GetSize("WordScrambleScores", "WordScrambleID", "WHERE UserID = 1");

            List<string> QuizScores = _parent.Database.ReadData("QuizScores", "Score", "USERID = 1", 1);
            List<string> PairsScores = _parent.Database.ReadData("PairsScores", "Score", "USERID = 1", 1);
            List<string> WordScrambleScores = _parent.Database.ReadData("WordScrambleScores", "Score", "USERID = 1", 1);

            int TotalQuizScore = 0;
            int TotalPairsScore = 0;
            int TotalWordScrambleScore = 0;


            foreach (var item in QuizScores)
            {
                TotalQuizScore += Int32.Parse(item);
            }

            foreach (var item in PairsScores)
            {
                TotalPairsScore += Int32.Parse(item);
            }

            foreach (var item in WordScrambleScores)
            {
                TotalWordScrambleScore += Int32.Parse(item);
            }

            try
            {
                QuizScoreAverage = TotalQuizScore / NumberOfQuizScores;
            }
            catch (Exception)
            {

            }
            
            try
            {
                PairsScoreAverage = TotalPairsScore / NumberOfPairsScores;
            }
            catch (Exception)
            {

            }
            
            try
            {
                WordScrambleScoreAverage = TotalWordScrambleScore / NumberOfWordScrambleScores;
            }
            catch (Exception)
            {

            }
            


        }

        void GetPercentile()
        {
            int NumberOfScores = 0;
            List<int> HighScores = new List<int>();
            int UserHS = 0;

            switch (_selectedHighScore)
            {
                case "Quiz HighScore":
                    NumberOfScores = _parent.Database.GetSize("QuizScores", "DISTINCT UserID", "");
                    for (int i = 1; i < NumberOfScores + 1; i++)
                    {

                        List<string> HighScore = _parent.Database.ReadData("QuizScores", "MAX(Score)", $"USERID = {i}", 1);
                        HighScores.Add(Int32.Parse(HighScore[0]));
                        if (i == _parent.UserID)
                        {
                            UserHS = Int32.Parse(HighScore[0]);
                        }

                    }
                    break;
                case "Pairs HighScore":
                    NumberOfScores = _parent.Database.GetSize("PairsScores", "DISTINCT UserID", "");
                    for (int i = 1; i < NumberOfScores + 1; i++)
                    {

                        List<string> HighScore = _parent.Database.ReadData("PairsScores", "MAX(Score)", $"USERID = {i}", 1);
                        HighScores.Add(Int32.Parse(HighScore[0]));
                        if (i == _parent.UserID)
                        {
                            UserHS = Int32.Parse(HighScore[0]);
                        }

                    }
                    break;
                case "Word Scramble HighScore":
                    NumberOfScores = _parent.Database.GetSize("WordScrambleScores", "DISTINCT UserID", "");
                    for (int i = 1; i < NumberOfScores + 1; i++)
                    {

                        List<string> HighScore = _parent.Database.ReadData("WordScrambleScores", "MAX(Score)", $"USERID = {i}", 1);
                        HighScores.Add(Int32.Parse(HighScore[0]));
                        if (i == _parent.UserID)
                        {
                            UserHS = Int32.Parse(HighScore[0]);
                        }

                    }
                    break;
                default:
                    break;
            }

            List<int> SortedHighScores = MergeSort.Sort(HighScores);

            int position = -1;
            for (int i = 0; i < SortedHighScores.Count; i++)
            {
                if (SortedHighScores[i] == UserHS)
                {
                    position = i;
                    break;
                }
            }
            if (position == 0)
            {
                _userPercentile = 0;
            }
            else if (position == -1)
            {
                MessageBox.Show("Oh no! :o Make sure you've selected a category!");
            }
            else
            {
                double temp = (double)position / (double)SortedHighScores.Count;
                double Percentile = temp * 100;
                _userPercentile = Percentile;
            }
            if (position != -1) 
            {
                MessageBox.Show($"You are better than {_userPercentile}% of users :)");
            }
            
            
        }


    }
}



