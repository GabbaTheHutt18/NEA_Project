﻿using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Linq;
using System.Net.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class AfricaMapViewModel:ObservableObject
    {
        //Initialise  
        private MainWindowViewModel _parent;
        public Database Africa { get; }
        private string _countryName;
        private string _countryPopulation;
        private string _countryLandArea;
        private string _countryDensity;
        private string _userInput;
        private List<string> _countries = new List<string>();

        public ICommand SearchButtonCommand { get; }
        public ICommand MapButtonCommand { get; }
        //constructor
        public AfricaMapViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            //will only populate database if its empty
            
            try
            {
                if (_parent.Database.GetSize("Africa", "ID", "") == 0)
                {
                    _parent.PopulateCountriesDatabase("Africa");
                }
            }
            catch (Exception e)
            {
                _parent.PopulateCountriesDatabase("Africa");
            }

            PopulateList();
            SearchButtonCommand = new SimpleCommand(_ => SearchButtonClickedCommand());
            MapButtonCommand = new SimpleCommand(_ => GoToMapPageCommand());
        }

        //assigning public to private, allows them to change if updated whilst the program is running
        public string CountryName { get=> _countryName; set { RaiseAndSetIfChanged(ref _countryName, value); } }
        public string CountryPopulation { get => _countryPopulation; set { RaiseAndSetIfChanged(ref _countryPopulation, value); } }
        public string CountryLandArea { get => _countryLandArea; set { RaiseAndSetIfChanged(ref _countryLandArea, value); } }
        public string CountryDensity { get => _countryDensity; set {RaiseAndSetIfChanged(ref _countryDensity, value); } }
        public string UserInput { get=> _userInput; set {RaiseAndSetIfChanged(ref _userInput, value); } }
        public List<string> Countries { get => _countries; }

        //Once a user has selected a Country, the database will be read and retrieves the information
        public void GetCountryInfo()
        {
            List<string> CountryInfo = _parent.Database.ReadData("Africa", "CountryName, Population, LandArea, Density", $"CountryName LIKE '{UserInput}'", 4);
            CountryName = CountryInfo[0];
            CountryPopulation = CountryInfo[1];
            CountryLandArea = CountryInfo[2];
            CountryDensity = CountryInfo[3];
        }
        
        //when the button is clicked GetCountryInfo is called 
        public void SearchButtonClickedCommand()
        { 
            GetCountryInfo();
        }

        //In order to populate the ComboBox, there must be a list of all the possible countries saved, this reads the database
        //and saves all the names to the list. 
        private void PopulateList()
        {

            for (int i = 0; i < _parent.Database.GetSize("Africa", "ID", ""); i++)
            {
                string country = _parent.Database.ReadData("Africa", "CountryName", $"ID = {i}",1)[0];
                _countries.Add(country);
            }

        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        public void GoToMapPageCommand()
        {
            _parent.ChangeToContinentsMap();        
        }
    }
}
