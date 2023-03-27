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
        private MainWindowViewModel _parent;

        public ICommand AfricaButtonClickedCommand { get; }
        public ICommand AsiaButtonClickedCommand { get; }
        public ICommand EuropeButtonClickedCommand { get; }
        public ICommand NAmericaButtonClickedCommand { get; }
        public ICommand SAmericaButtonClickedCommand { get; }
        public ICommand OceaniaButtonClickedCommand { get; }
        public ICommand HomePageButtonCommandClicked { get; }
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

        private void AfricaButtonClicked()
        {
            _parent.ChangeToAfricaMap();
        }
        private void AsiaButtonClicked()
        {
            _parent.ChangeToAsiaMap();
        }
        private void EuropeButtonClicked()
        {
            _parent.ChangeToEuropeMap();
        }
        private void NAmericaButtonClicked()
        {
            _parent.ChangeToNAmericaMap();
        }
        private void SAmericaButtonClicked()
        {
            _parent.ChangeToSAmericaMap();
        }
        private void OceaniaButtonClicked()
        {
            _parent.ChangeToOceaniaMap();
        }
        private void HomePageButtonClicked()
        {
            _parent.ChangeToHomePage();
        }
    }
}
