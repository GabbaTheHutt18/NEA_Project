using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Project.ViewModels
{
    public class UserStatsPageViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;
        //private List<string[]> QuestionBank = new List<string[]>();
        public UserStatsPageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
        }
    }
}
