﻿using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class QuizPageViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;
        
        public QuizPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
        }
    }
}
