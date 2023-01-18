using NEA_Project.Helpers;
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

        public HomePageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
        
        }

    }
}
