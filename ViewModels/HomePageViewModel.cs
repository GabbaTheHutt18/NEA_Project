﻿using NEA_Project.Helpers;
using SQLDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;

        public ICommand MapButtonClickedCommand { get; }
        public ICommand QuestionBankMenuButtonClickedCommand { get; }

        public ICommand GameMenuButtonClickedCommand { get; }

        public HomePageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            MapButtonClickedCommand = new SimpleCommand(_ => MapButtonClicked());
            QuestionBankMenuButtonClickedCommand = new SimpleCommand(_ => QuestionBankButtonClicked());
            GameMenuButtonClickedCommand = new SimpleCommand(_ => GameMenuButtonClicked());
        }

        private void MapButtonClicked()
        {
            _parent.ChangeToContinentsMap();
        }

        private void QuestionBankButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
        private void GameMenuButtonClicked()
        {
            _parent.ChangeToGameMenuPage();
        }

    }
}
