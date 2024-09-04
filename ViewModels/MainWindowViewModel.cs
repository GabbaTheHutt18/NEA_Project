using NEA_Project.Constants;
using NEA_Project.Helpers;
using NEA_Project.Pages;
using SQLDatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        //Initialise
        private ViewStates _currentPage = ViewStates.StartPage;
        
        public Database Database = new Database();
        public LoginPageViewModel LoginPageViewModel { get; set; }
        public StartPageViewModel StartPageViewModel { get; set; }
        public SignUpPageViewModel SignUpPageViewModel { get; set; }
        public HomePageViewModel HomePageViewModel { get; set; }    

        public ContinentsMapViewModel ContinentsMapViewModel { get; set; }
        public AfricaMapViewModel AfricaMapViewModel { get; set; }
        public AsiaMapViewModel AsiaMapViewModel { get; set; }
        public EuropeMapViewModel EuropeMapViewModel { get; set; }
        public NAmericaMapViewModel NAmericaMapViewModel { get; set; }
        public SAmericaMapViewModel SAmericaMapViewModel { get; set; }
        public OceaniaMapViewModel OceaniaMapViewModel { get; set; }

        public QuestionBankMenuViewModel QuestionBankMenuViewModel { get; set; }
        public QuestionBankEditViewModel QuestionBankEditViewModel { get; set; }
        public QuestionBankCreateViewModel QuestionBankCreateViewModel { get; set; }
        public QuestionBankDeleteViewModel QuestionBankDeleteViewModel { get; set; }
        public QuestionBankReadViewModel QuestionBankReadViewModel { get; set; }
        public UserStatsViewModel UserStatsViewModel { get; set; }

        public GameMenuViewModel GameMenuViewModel { get; set; }
        public QuizViewModel QuizViewModel { get; set; }
        public WordScrambleViewModel WordScrambleViewModel { get; set; }
        public PairsGameViewModel PairsGameViewModel { get; set; }
       

        public int UserID { get; set; }
        

        public ViewStates CurrentPage 
        {
            get => _currentPage;
            set
            { 
                RaiseAndSetIfChanged(ref _currentPage, value);
            }
        }
        private string _currentQuestionBank = "Default";
        public string CurrentQuestionBank { get =>_currentQuestionBank; set { _currentQuestionBank = value; } }
        //Constructor
        public MainWindowViewModel()
        {
            PopulateQuestionBank();
            Database.CreateTable("LoginDetails", "UserID INT, UserNames VARCHAR(20), Passwords VARCHAR(500)");
            Database.CreateTable("QuestionBanks", "UserID INT, BankName VARCHAR(100), QuestionID INT, Question VARCHAR(100), Answer VARCHAR(150)");
            Database.CreateTable("QuizScores", "QuizID INT, UserID INT, Score VARCHAR(100)");
            Database.CreateTable("PairsScores", "PairsID INT, UserID INT, Score VARCHAR(100)");
            Database.CreateTable("WordScrambleScores", "WordScrambleID INT, UserID INT, Score VARCHAR(100)");
            
            LoginPageViewModel = new LoginPageViewModel(this);
            StartPageViewModel = new StartPageViewModel(this);
            SignUpPageViewModel = new SignUpPageViewModel(this);
            HomePageViewModel = new HomePageViewModel(this);
            ContinentsMapViewModel = new ContinentsMapViewModel(this);
            AfricaMapViewModel = new AfricaMapViewModel(this);
            AsiaMapViewModel = new AsiaMapViewModel(this);
            EuropeMapViewModel = new EuropeMapViewModel(this);
            NAmericaMapViewModel = new NAmericaMapViewModel(this);
            SAmericaMapViewModel = new SAmericaMapViewModel(this);
            OceaniaMapViewModel = new OceaniaMapViewModel(this); 
            QuestionBankMenuViewModel = new QuestionBankMenuViewModel(this);
            QuestionBankEditViewModel = new QuestionBankEditViewModel(this);
            QuestionBankCreateViewModel = new QuestionBankCreateViewModel(this);
            QuestionBankDeleteViewModel = new QuestionBankDeleteViewModel(this);
            QuestionBankReadViewModel = new QuestionBankReadViewModel(this);
            UserStatsViewModel = new UserStatsViewModel(this);
            GameMenuViewModel = new GameMenuViewModel(this);
            QuizViewModel = new QuizViewModel(this);
            WordScrambleViewModel = new WordScrambleViewModel(this);
            PairsGameViewModel = new PairsGameViewModel(this);
            

        }

        //When called the Current Page is assigned the ViewStates of the corresponding UserControl, this enables the MainWindow to update
        public void ChangeToLoginPage()
        {
           
            CurrentPage = ViewStates.LoginPage;
        }

        public void ChangeToSignUpPage()
        {
            
            CurrentPage = ViewStates.SignUpPage;
        }

        public void ChangeToHomePage()
        {
            CurrentPage = ViewStates.HomePage;
        }

        public void ChangeToContinentsMap()
        {
            CurrentPage = ViewStates.ContinentsMap;
        

        }

        public void ChangeToAfricaMap()
        {
            CurrentPage = ViewStates.AfricaMap;
        }

        public void ChangeToAsiaMap()
        {
            CurrentPage = ViewStates.AsiaMap;
        }

        public void ChangeToEuropeMap()
        {
            CurrentPage = ViewStates.EuropeMap;
        }
        public void ChangeToNAmericaMap()
        {
            CurrentPage = ViewStates.NAmericaMap;
        }
        public void ChangeToSAmericaMap()
        {
            CurrentPage = ViewStates.SAmericaMap;
        }
        public void ChangeToOceaniaMap()
        {
            CurrentPage = ViewStates.OceaniaMap;
        }

        public void ChangeToQuestionBankMenuPage()
        { 
            CurrentPage = ViewStates.QuestionBankMenu;
        }

        public void ChangeToQuestionBankEditPage()
        {
            CurrentPage = ViewStates.QuestionBankEdit;
        }
        public void ChangeToQuestionBankCreatePage()
        {
            CurrentPage = ViewStates.QuestionBankCreate;
        }
        public void ChangeToQuestionBankDeletePage()
        {
            CurrentPage = ViewStates.QuestionBankDelete;
        }
        public void ChangeToQuestionBankReadPage()
        {
            CurrentPage = ViewStates.QuestionBankRead;
        }
        public void ChangeToUserStatsPage()
        {
            CurrentPage = ViewStates.UserStatsPage;
        }
        public void ChangeToGameMenuPage()
        {
            CurrentPage = ViewStates.GameMenuPage;
        }
        public void ChangeToQuizPage()
        {
            CurrentPage = ViewStates.QuizPage;
        }
        public void ChangeToWordScramblePage()
        {
            CurrentPage = ViewStates.WordScramblePage;
        }
        public void ChangeToPairsGamePage()
        {
            CurrentPage = ViewStates.PairsGamePage;
        }



        //Using the inbuilt Hashing function SHA256, gets string input, converts into an array of ascii value, returns the hashed value

        public byte[] Hashing(string userInput)
        {
            byte[] ascii = new byte[userInput.Length];
            for (int i = 0; i < userInput.Length; i++)
            {

                ascii[i] = (byte)userInput[i];

            }

            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(ascii);
            for (int j = 0; j < result.Length; j++)
            {
                Console.WriteLine(result[j]);
            }
            return result;
        }

        //Gets the which continent, then reads a text file, which then is split into different arrays, before being added to the database
        public void PopulateCountriesDatabase(string Continent)
        {
            string TopFolder = @" ..\..\Content";
     
            string filename = Continent + ".txt";
            string fullPath = Path.Combine(TopFolder, filename);
            string[] lines = File.ReadAllLines(fullPath);
            List<string[]> test = new List<string[]>();
            Database.CreateTable($"{Continent}", "ID INT, CountryName VARCHAR(60),Population VARCHAR(100), LandArea VARCHAR(100), Density VARCHAR(100)");
            int count = 0;
            foreach (var item in lines)
            {
                test.Add(item.Split(","));
            }
            if (Database.GetSize(Continent, "ID", "") == 0)
            {
                foreach (var item in test)
                {
                    Database.InsertData($"{Continent}", "ID, CountryName,Population,LandArea,Density", $"{count},'{item[0]}','{item[1]}','{item[2]}','{item[3]}'");
                    count += 1;
                }
            }
       
        }

        //Reads a text file (containing the default questions), then is split into different arrays, before being added a list of arrays
        public List<string[]> GetTheText()
        {
            List<string[]> FileContent = new List<string[]>();


            string TopFolder = @"../../../Content";
            string filename = "Default.txt";
            string fullPath = Path.Combine(TopFolder, filename);

            string[] lines = File.ReadAllLines(fullPath);

            int length = 0;

            foreach (var item in lines)
            {
                item[length].ToString().Trim();
                FileContent.Add(item.Split(","));
            }
            return FileContent;

        }

        //Using the default text file, the Questions and Answers are saved to the database, alongside a unique Question ID
        public void PopulateQuestionBank()
        {
            
            int size = Database.GetSize("QuestionBanks", "QuestionID", "");
            //If Question Banks is empty then populate the database
            if (size == 0)
            {
                List<string[]> FileContent = GetTheText();
                for (int i = 0; i < FileContent.Count; i++)
                {
                    Database.InsertData($"QuestionBanks", "UserID,BankName,QuestionID,Question,Answer", $"0,'Default', '{i + 1}','{FileContent[i][0]}','{FileContent[i][1]}'");
                    
                }
            }

        }

    }


}
