using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class AsiaMapViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;
        public Database Asia { get; }
        private string _countryName;
        private string _countryPopulation;
        private string _countryLandArea;
        private string _countryDensity;
        private string _userInput;
        private List<string> _countries = new List<string>();
        public ICommand SearchButtonCommand { get; }
        public ICommand MapButtonCommand { get; }
        public AsiaMapViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            if (Asia == null)
            {
                Asia = _parent.PopulateCountriesDatabase("Asia");
            }
            PopulateList();
            SearchButtonCommand = new SimpleCommand(_ => SearchButtonClickedCommand());
            MapButtonCommand = new SimpleCommand(_ => GoToMapPageCommand());

        }
        public string CountryName { get => _countryName; set { RaiseAndSetIfChanged(ref _countryName, value); } }
        public string CountryPopulation { get => _countryPopulation; set { RaiseAndSetIfChanged(ref _countryPopulation, value); } }
        public string CountryLandArea { get => _countryLandArea; set { RaiseAndSetIfChanged(ref _countryLandArea, value); } }
        public string CountryDensity { get => _countryDensity; set { RaiseAndSetIfChanged(ref _countryDensity, value); } }
        public string UserInput { get => _userInput; set { RaiseAndSetIfChanged(ref _userInput, value); } }
        public List<string> Countries { get => _countries; }

        public void GetCountryInfo()
        {
            CountryName = Asia.ReadData("Asia", "CountryName", $"CountryName LIKE '{UserInput}'");
            CountryPopulation = Asia.ReadData("Asia", "Population", $"CountryName LIKE '{UserInput}'");
            CountryLandArea = Asia.ReadData("Asia", "LandArea", $"CountryName LIKE '{UserInput}'");
            CountryDensity = Asia.ReadData("Asia", "Density", $"CountryName LIKE '{UserInput}'");
        }


        public void SearchButtonClickedCommand()
        {
            GetCountryInfo();
        }
        private void PopulateList()
        {

            for (int i = 0; i < Asia.GetSize("Asia", "ID"); i++)
            {
                string country = Asia.ReadData("Asia", "CountryName", $"ID = {i}");
                _countries.Add(country);
            }

        }
        private void GoToMapPageCommand()
        {
            _parent.ChangeToContinentsMap();
        }
    }
}
