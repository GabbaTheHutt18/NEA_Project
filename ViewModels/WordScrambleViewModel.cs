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
    public class WordScrambleViewModel : ObservableObject
    {
        //Inititalise
        MainWindowViewModel _parent;
        private string _answer;
        private string _userInput;
        private string _continent;
        private string _scrambledWord;
        private string _userNameInput = "Enter Your Username Here...";
        private string _passwordInput = "Enter Your Password Here...";
        private int _score = 0;
        public int UserID = 1;
        public Random random = new Random();

        public ICommand GenerateQuestionButtonClickedCommand { get; }
        public ICommand CheckAnswerCommand { get; }
        public ICommand FinishButtonCommand { get; }

        public ICommand Hint1ButtonClickedCommand { get; }
        public ICommand Hint2ButtonClickedCommand { get; }
        //Constructor
        public WordScrambleViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            GenerateQuestionButtonClickedCommand = new SimpleCommand(_ => GenerateQuestionButtonClicked());
            CheckAnswerCommand = new SimpleCommand(_ => CheckAnswer());
            FinishButtonCommand = new SimpleCommand(_ => FinishButtonClicked());
            Hint1ButtonClickedCommand = new SimpleCommand(_=> Hint1ButtonClicked());
            Hint2ButtonClickedCommand = new SimpleCommand((_ => Hint2ButtonClicked()));
        }

        public string Answer { get { return _answer; } set {  _answer = value; } }
        
        //RaiseAndSetIfChanged -> Allows the variables to update
        public string ScrambledWord
        {
            get => _scrambledWord;
            set
            {
                RaiseAndSetIfChanged(ref _scrambledWord, value);
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

            GetAnagram();

        }
        //When the Finished button is clicked, the new score is compared to the 
        // existing one in the database and if it is greater the database is 
        // updated and the page changes to the home page
        private void FinishButtonClicked()
        {
            int existingScore = 0;
            try
            {
                string DatabaseScore = _parent.Database.ReadData("UserStats", "HighScore3",
                    $"USERID = {_parent.UserID}", 1)[0];
                existingScore = Int32.Parse(DatabaseScore);
            }
            catch (Exception)
            {


            }
            if (existingScore < Score)
            {
                _parent.Database.UpdateData("UserStats", $"HighScore3 = {Score}", 
                    $"USERID = {_parent.UserID}");
            }
            Score = 0;
            _parent.ChangeToGameMenuPage();
        }

        private void Hint1ButtonClicked()
        {
            MessageBox.Show($"This country is in: {_continent}");
        }

        private void Hint2ButtonClicked()
        {
            MessageBox.Show($"This country starts with: {_answer[0]}");
        }

        //The users answer is compared to the correct
        //answer and the score is updated accordingly
        public void CheckAnswer()
        {
            try
            {
                if (UserInput != null && Answer != null)
                {
                    //checks that both user input and answer are not null
                    if (UserInput.Trim().ToUpper() == Answer.Trim().ToUpper())
                    {
                        MessageBox.Show("yay!");
                        Score += 1;
                    }
                    else
                    {
                        MessageBox.Show($"neigh </3 {Answer}");
                        Score -= 1;
                    }
                   
                }
                
            }
            catch (Exception)
            {

                
            }
            

        }

      
        private void GetAnagram()
        {
            //Gets random number and depending on the
            //number, this returns a random country
            Random random = new Random();

            int randomContinent = random.Next(6);

            string tableName = string.Empty;
            switch (randomContinent)
            {
                case 0:
                    tableName = "Africa";
                    break;
                case 1:
                    tableName = "Asia";
                    break;
                case 2:
                    tableName = "Europe";
                    break;
                case 3:
                    tableName = "NorthAmerica";
                    break;
                case 4:
                    tableName = "SouthAmerica";
                    break;
                case 5:
                    tableName = "Oceania";
                    break;
                default:
                    break;
            }
            _continent = tableName;
            //get the size of the database and then get a random country because 
            int NoCountries = _parent.Database.GetSize($"{tableName}", "ID","");
            int RandomCountry = random.Next(NoCountries);
            _answer = _parent.Database.ReadData($"{tableName}", "CountryName", 
                $"ID = {RandomCountry}",1)[0].ToLower();
            //Get the list of all possible combinations of the Country name
            // Gets rid of the answer from the possible list to avoid that being selected randomly
            //Randomly selects a scrambled world
            List<string> ScrambledWords = new List<string>();
            Permute(_answer.ToCharArray(), 0, _answer.Length - 1, ref ScrambledWords);
            ScrambledWords.Remove(_answer);
            int randomInt = random.Next(ScrambledWords.Count);
            ScrambledWord = ScrambledWords[randomInt];
            
        }

        private void Permute(char[] arr, int start, int end, ref List<string> list)
        {
            //when a new combination is found, its added to the list
            if (start == end)
            {
                
                list.Add(new string(arr));
            }
            else
            {
                //cycles through the word (arr) and swaps each character 
                for (int j = start; j <= end; j++)
                {
                    Swap(ref arr[start], ref arr[j]);
                    Permute(arr, start + 1, end, ref list);
                    Swap(ref arr[start], ref arr[j]);
                }
            }
        }

        static void Swap(ref char a, ref char b)
        {
            char temp = a;
            a = b;
            b = temp;
        }
    }
}
