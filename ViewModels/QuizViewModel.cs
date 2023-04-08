using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Project.ViewModels
{
    public class QuizViewModel
    {
        MainWindowViewModel _parent;
        public QuizViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
        }
    }
}
