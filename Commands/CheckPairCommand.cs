using MVVMEssentials.Commands;
using NEA_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NEA_Project.Commands
{
    public class CheckPairCommand : CommandBase
    {
        //Intialising attributes
        private readonly PairsGameViewModel _vm;
        private MainWindowViewModel _parent;
        private bool found = false;
        //constructor 
        public CheckPairCommand(PairsGameViewModel vm, MainWindowViewModel parent)
        {
            _parent = parent;
            _vm = vm;
        }

        //This method overrides the execute method of CommandBase and is how the command actually executes. 
        public override void Execute(object parameter)
        {
            _vm.PairFound = false;
            RightPair();
            //check to see if a pair has been found, then the score is updated accordingly and a message box is displayed
            if (found)
            {
                MessageBox.Show("Pair!");
                _vm.Score += 1;
                _vm.PairFound = true;
            }
            else
            {
                MessageBox.Show($"Not Pair");
                _vm.Score -= 1;
            }


        }

        private bool RightPair()
        {
            // This compares the content of the two textboxes with the expected answers in the database

            int ID;
            if (_parent.CurrentQuestionBank == "Default")  
            {
                ID = 0;
                //Because of the way that the Question banks are saved, this statement means  
                //that it is possible for all users to access the Default Question Bank
            }
            else
            {
                ID = _parent.UserID;
            }
            found = false;
            //returns the expected answers, but because you don't know which textblock
            //is in the "Answer" column and which is in the "Question", have to
            // create 2 variables to cover both scenarios 
            string textblock2Contains = _vm.TextBlock2Contains;
            List<string> CheckWithTextBlock = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question LIKE '{_vm.TextBlockContains}' AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}", 1);
            
            string textblockContains = _vm.TextBlockContains;
            List<string> CheckWithTextBlock2 = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question LIKE '{_vm.TextBlock2Contains}' AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}", 1);

            // Because the ReadData method sometimes returns null
            // this IF avoids an exception error as it
            // only compares the textblocks content with the answer that is not null. 

            if (CheckWithTextBlock.Count == 0) 
            {
                if (textblockContains == CheckWithTextBlock2[0]) 
                {
                    found = true;
                }
            }
            else
            {
                if (textblock2Contains == CheckWithTextBlock[0]) found = true;

            }

            return found;

        }
    }
}
