using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Project.ViewModels
{
    public class PairsGamePageViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;
        
        public PairsGamePageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
        }
    }
}
