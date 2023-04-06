using NEA_Project.Helpers;
using NEA_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class QuestionBankMenuPageViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;
        public ICommand CreateButtonClickedCommand { get; }
        public ICommand DeleteButtonClickedCommand { get; }
        public ICommand ReadButtonClickedCommand { get; }
        public ICommand EditButtonClickedCommand { get; }
        public ICommand HomeButtonClickedCommand { get; }

        public QuestionBankMenuPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            CreateButtonClickedCommand = new SimpleCommand(_ => CreateButtonClicked());
            DeleteButtonClickedCommand = new SimpleCommand(_ => DeleteButtonClicked());
            ReadButtonClickedCommand = new SimpleCommand(_ => ReadButtonClicked());
            EditButtonClickedCommand = new SimpleCommand(_ => EditButtonClicked());
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
        }


        private void CreateButtonClicked()
        {
            _parent.ChangeToQuestionBankCreatePage();

        }
        private void DeleteButtonClicked()
        {
            _parent.ChangeToQuestionBankDeletePage();

        }

        private void ReadButtonClicked()
        {
            _parent.ChangeToQuestionBankReadPage();
        }
        private void EditButtonClicked()
        {
            _parent.ChangeToQuestionBankEditPage();
        }

        private void HomeButtonClicked()
        {
            _parent.ChangeToHomePage();
        }

    }
}
