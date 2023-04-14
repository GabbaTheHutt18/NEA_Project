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
        MainWindowViewModel _parent;
        private double _userPercentile;
        Dictionary<int, List<int>> HighScores = new Dictionary<int, List<int>>();
        List<int> HighScore1 = new List<int>();
        List<int> HighScore2 = new List<int>();
        List<int> HighScore3 = new List<int>();
        List<Dictionary<int, int>> SortedHighScores1 = new List<Dictionary<int, int>>();
        List<Dictionary<int, int>> SortedHighScores2 = new List<Dictionary<int, int>>();
        List<Dictionary<int, int>> SortedHighScores3 = new List<Dictionary<int, int>>();
        public MergeSort MergeSort;
        public double HowMuchBetterHS1;
        public double HowMuchBetterHS2;
        public double HowMuchBetterHS3;
        private string _quizHighScore;
        private string _pairsHighScore;
        private string _wordScrambleHighScore;
        private List<string> _highScoreCategories = new List<string>() 
        { "Quiz HighScore", "Pairs HighScore", "Word Scramble HighScore"};
        private string _selectedHighScore;
        public ICommand GetUserStatsCommand { get; }
        public ICommand GetPercentagesCommand { get; }
        public ICommand HomeButtonClickedCommand { get; }
        public UserStatsViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            MergeSort = new MergeSort();
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
            GetUserStatsCommand = new SimpleCommand(_=> GetUserStats());
            GetPercentagesCommand = new SimpleCommand(_ => GetPercentage());
        }
        private void HomeButtonClicked()
        {
            _parent.ChangeToHomePage();
        }
        public string QuizHighScore { get => _quizHighScore; set { RaiseAndSetIfChanged(ref _quizHighScore, value); } }
        public string PairsHighScore { get => _pairsHighScore; set { RaiseAndSetIfChanged(ref _pairsHighScore, value); } }
        public string WordHighScore { get => _wordScrambleHighScore; set { RaiseAndSetIfChanged(ref _wordScrambleHighScore, value); } }
        public List<string> HighScoresCategories { get => _highScoreCategories; }

        public string selectedValue
        {
            get => _selectedHighScore;
            set { _selectedHighScore = value; }
        }

        public void GetUserStats()
        {
            QuizHighScore = _parent.Database.ReadData("UserStats","HighScore1",$"UserID = {_parent.UserID}",1)[0];
            PairsHighScore = _parent.Database.ReadData("UserStats", "HighScore2", $"UserID = {_parent.UserID}", 1)[0];
            WordHighScore = _parent.Database.ReadData("UserStats", "HighScore3", $"UserID = {_parent.UserID}", 1)[0];
        }
        
        public void GetPercentage()
        {
            PopulateUnsortedLists();
            //Quiz HighScore", "Pairs HighScore", "Word Scramble HighScore
            if (_selectedHighScore == "Quiz HighScore")
            {
                SortedHighScores1 = PopulateSortedLists(HighScore1, 0);
                HowMuchBetterHS1 = CalculatePercentile(SortedHighScores1, _parent.UserID);
                MessageBox.Show($"User is {HowMuchBetterHS1}% better than other users at Quiz");
            }
            else if (_selectedHighScore == "Pairs HighScore")
            {
                SortedHighScores2 = PopulateSortedLists(HighScore2,1);
                HowMuchBetterHS2 = CalculatePercentile(SortedHighScores2, _parent.UserID);
                MessageBox.Show($"User is {HowMuchBetterHS2}% better than other users at Pairs");
            }
            else if (_selectedHighScore == "Word Scramble HighScore")
            {
                SortedHighScores3 = PopulateSortedLists(HighScore3, 2);
                HowMuchBetterHS3 = CalculatePercentile(SortedHighScores3, _parent.UserID);
                MessageBox.Show($"User is {HowMuchBetterHS3}% better than other users at Word Scramble");
            }
            
            
        }

        public void PopulateUnsortedLists()
        {
            HighScores.Clear();
            for (int i = 0; i < _parent.Database.GetSize("UserStats", "UserID", ""); i++)
            {
                
                List<int> hs = PopulateDictionary();
                HighScores[i + 1] = hs;
                Console.WriteLine(i);
                HighScore1.Add(HighScores[i + 1][0]);
                HighScore2.Add(HighScores[i + 1][1]);
                HighScore3.Add(HighScores[i + 1][2]); //forgot to add 1 
                
                
                 

            }

        }

        public List<Dictionary<int, int>> PopulateSortedLists(List<int> HighScore, int i)
        {
            MergeSort.Sort(HighScore);
            List<Dictionary<int, int>> SortedHighScores = new List<Dictionary<int, int>>();
            

            foreach (var item in HighScore)
            {

                foreach (var ID in HighScores)
                {
                    if (ID.Value[i] == item)
                    {
                        
                        try
                        {
                            //SortedHighScores1[ID.Key] = item;
                            Dictionary<int, int> temp = new Dictionary<int, int>() { { ID.Key, item } };
                            SortedHighScores.Add(temp);

                        }
                        catch (Exception)
                        {

                        }

                    }
                }

            }
            return SortedHighScores;
        }

        public List<int> PopulateDictionary()
        {
            List<string> temp = _parent.Database.ReadData("UserStats", "HighScore1, HighScore2, HighScore3", $"UserID = {_parent.UserID}",3);
            List<int> HighScores = new List<int>();
            for (int i = 0; i < temp.Count; i++)
            {
                if (!HighScores.Contains(Int32.Parse(temp[i])))
                {
                    HighScores.Add(Int32.Parse(temp[i]));
                }
                
            }


            return HighScores;
        }

        public double CalculatePercentile(List<Dictionary<int, int>> HighScores, int UserID)
        {
            _userPercentile = 0;
            int position = -1;
            for (int i = 0; i < HighScores.Count; i++)
            {
                if (HighScores[i].ContainsKey(UserID))
                {
                    position = i;
                    break;
                }
            }
            double temp = (double)position / HighScores.Count; //NEED TO CONVERT TO DOUBLE (INT DIVISION)
            double Percentile = temp * 100;

            _userPercentile = 100 - Percentile;
            return _userPercentile;
        }
    }
}
