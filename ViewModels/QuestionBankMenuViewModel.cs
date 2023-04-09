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
        private MainWindowViewModel _parent;
        public ICommand HomeButtonClickedCommand { get; }
        public ICommand CreateButtonClickedCommand { get; }
        public ICommand DeleteButtonClickedCommand { get; }
        public ICommand ReadButtonClickedCommand { get; }
        public ICommand EditButtonClickedCommand { get; }


        public QuestionBankMenuViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            HomeButtonClickedCommand = new SimpleCommand(_ => HomeButtonClicked());
            CreateButtonClickedCommand = new SimpleCommand(_ => CreateButtonClicked());
            DeleteButtonClickedCommand = new SimpleCommand(_ => DeleteButtonClicked());
            ReadButtonClickedCommand = new SimpleCommand(_ => ReadButtonClicked());
            EditButtonClickedCommand = new SimpleCommand(_ => EditButtonClicked());
        }

        private void HomeButtonClicked()
        {
            _parent.ChangeToHomePage();
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

    }
}
