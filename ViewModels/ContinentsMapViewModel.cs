using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class ContinentsMapViewModel
    {
        //Initialise
        private MainWindowViewModel _parent;
        public ICommand AfricaButtonClickedCommand { get; }
        public ICommand AsiaButtonClickedCommand { get; }
        public ICommand EuropeButtonClickedCommand { get; }
        public ICommand NAmericaButtonClickedCommand { get; }
        public ICommand SAmericaButtonClickedCommand { get; }
        public ICommand OceaniaButtonClickedCommand { get; }
        public ICommand HomePageButtonCommandClicked { get; }
        //Constructor
        public ContinentsMapViewModel (MainWindowViewModel parent)
        {
            _parent = parent;
            AfricaButtonClickedCommand = new SimpleCommand(_ => AfricaButtonClicked());
            AsiaButtonClickedCommand = new SimpleCommand(_ => AsiaButtonClicked());
            EuropeButtonClickedCommand = new SimpleCommand(_ => EuropeButtonClicked());
            NAmericaButtonClickedCommand = new SimpleCommand(_ => NAmericaButtonClicked());
            SAmericaButtonClickedCommand = new SimpleCommand(_ => SAmericaButtonClicked());
            OceaniaButtonClickedCommand = new SimpleCommand(_ => OceaniaButtonClicked());
            HomePageButtonCommandClicked = new SimpleCommand(_ => HomePageButtonClicked());


        }

        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void AfricaButtonClicked()
        {
            _parent.ChangeToAfricaMap();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void AsiaButtonClicked()
        {
            _parent.ChangeToAsiaMap();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void EuropeButtonClicked()
        {
            _parent.ChangeToEuropeMap();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void NAmericaButtonClicked()
        {
            _parent.ChangeToNAmericaMap();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void SAmericaButtonClicked()
        {
            _parent.ChangeToSAmericaMap();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void OceaniaButtonClicked()
        {
            _parent.ChangeToOceaniaMap();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void HomePageButtonClicked()
        {
            _parent.ChangeToHomePage();
        }
    }
}
