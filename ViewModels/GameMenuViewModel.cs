using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Project.ViewModels
{
    public class GameMenuViewModel
    {
        MainWindowViewModel _parent;
        public GameMenuViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
        }
    }
}
