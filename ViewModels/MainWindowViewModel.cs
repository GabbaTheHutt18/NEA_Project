using NEA_Project.Constants;
using NEA_Project.Helpers;
using NEA_Project.Pages;
using SQLDatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
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

        public QuestionBankMenuPageViewModel QuestionBankMenuPageViewModel { get; set; }
        public QuestionBankCreatePageViewModel QuestionBankCreatePageViewModel { get; set; }
        public QuestionBankDeletePageViewModel QuestionBankDeletePageViewModel { get; set; }
        public QuestionBankEditPageViewModel QuestionBankEditPageViewModel { get; set; }
        public QuestionBankReadPageViewModel QuestionBankReadPageViewModel { get; set; }
        
        public GameMenuPageViewModel GameMenuPageViewModel { get; set; }
        public PairsGamePageViewModel PairsGamePageViewModel { get; set; }
        public QuizPageViewModel QuizPageViewModel { get; set; }
        public WordScramblePageViewModel WordScramblePageViewModel { get; set; }
        public UserStatsPageViewModel UserStatsPageViewModel { get; set; }

        public int UserID { get; set; }

        public ViewStates CurrentPage 
        {
            get => _currentPage;
            set
            { 
                RaiseAndSetIfChanged(ref _currentPage, value);
            }
        }
        public MainWindowViewModel()

        {
            Database.CreateTable("LoginDetails", "UserNames VARCHAR(20), Passwords VARCHAR(500)");
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
            QuestionBankMenuPageViewModel = new QuestionBankMenuPageViewModel(this);
            QuestionBankCreatePageViewModel = new QuestionBankCreatePageViewModel(this);
            QuestionBankDeletePageViewModel = new QuestionBankDeletePageViewModel(this);
            QuestionBankEditPageViewModel = new QuestionBankEditPageViewModel(this);
            QuestionBankReadPageViewModel = new QuestionBankReadPageViewModel(this);
            GameMenuPageViewModel = new GameMenuPageViewModel(this);
            PairsGamePageViewModel = new PairsGamePageViewModel(this);
            QuizPageViewModel = new QuizPageViewModel(this);
            WordScramblePageViewModel = new WordScramblePageViewModel(this);
            UserStatsPageViewModel = new UserStatsPageViewModel(this);
       


        }

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
        public void ChangeToQuestionBankCreatePage()
        {
            CurrentPage = ViewStates.QuestionBankCreate;
        }
        public void ChangeToQuestionBankDeletePage()
        {
            CurrentPage = ViewStates.QuestionBankDelete;
        }
        public void ChangeToQuestionBankEditPage()
        {
            CurrentPage = ViewStates.QuestionBankEdit;
        }
        public void ChangeToQuestionBankReadPage()
        {
            CurrentPage = ViewStates.QuestionBankRead;
        }

        public void ChangeToGameMenuPage()
        {
            CurrentPage = ViewStates.GameMenuPage;
        }

        public void ChangeToPairsGamePage()
        {
            CurrentPage = ViewStates.PairsGamePage;
        }
        public void ChangeToQuizPage()
        {
            CurrentPage = ViewStates.QuizPage;
        }
        public void ChangeToWordScramblePage()
        {
            CurrentPage = ViewStates.WordScramblePage;
        }

        public void ChangeToUserStatsPage()
        {
            CurrentPage = ViewStates.UserStatsPage;
        }


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

        public void PopulateCountriesDatabase(string Continent)
        {
            string TopFolder = @"../../../Content";
            string filename = Continent + ".txt";
            string fullPath = Path.Combine(TopFolder, filename);
            //string fullPath = System.IO.Path.GetFullPath($"{filename}.txt");
            string[] lines = File.ReadAllLines(fullPath);
            List<string[]> test = new List<string[]>();
            Hashtable help = new Hashtable();
            Dictionary<int, string[]> countriesTest = new Dictionary<int, string[]>();
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
            
            
            
            

            //return countries;
        }


    }

    
}
