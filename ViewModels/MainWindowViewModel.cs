using NEA_Project.Constants;
using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
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
        public Database LoginDataBase = new Database("LoginDetails", "UserNames VARCHAR(20), Passwords VARCHAR(500)");
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


    }

    
}
