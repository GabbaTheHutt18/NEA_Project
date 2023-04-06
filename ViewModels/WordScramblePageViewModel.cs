using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Project.ViewModels
{
    public class WordScramblePageViewModel : ObservableObject
    {
        private MainWindowViewModel _parent;
        //private List<string[]> QuestionBank = new List<string[]>();
        public WordScramblePageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
        }
    }
}
