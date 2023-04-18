using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class QuestionBankMenuViewModel
    {
        //initialise 
        private MainWindowViewModel _parent;
        public ICommand HomeButtonClickedCommand { get; }
        public ICommand CreateButtonClickedCommand { get; }
        public ICommand DeleteButtonClickedCommand { get; }
        public ICommand ReadButtonClickedCommand { get; }
        public ICommand EditButtonClickedCommand { get; }

        //constructor
        public QuestionBankMenuViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
            CreateButtonClickedCommand = new SimpleCommand(_ => CreateButtonClicked());
            DeleteButtonClickedCommand = new SimpleCommand(_ => DeleteButtonClicked());
            ReadButtonClickedCommand = new SimpleCommand(_ => ReadButtonClicked());
            EditButtonClickedCommand = new SimpleCommand(_ => EditButtonClicked());
        }

        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void HomeButtonClicked()
        {
            _parent.ChangeToHomePage();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void CreateButtonClicked()
        {
            _parent.ChangeToQuestionBankCreatePage();

        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void DeleteButtonClicked()
        {
            _parent.ChangeToQuestionBankDeletePage();

        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void ReadButtonClicked()
        {
            _parent.ChangeToQuestionBankReadPage();
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void EditButtonClicked()
        {
            _parent.ChangeToQuestionBankEditPage();
        }

    }
}
