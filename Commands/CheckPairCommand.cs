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
        private readonly PairsGameViewModel _vm;
        private MainWindowViewModel _parent;
        private List<string[]> _QnA;
        private bool found = false;

        public CheckPairCommand(PairsGameViewModel vm, MainWindowViewModel parent)
        {
            _parent = parent;
            _vm = vm;
            _QnA = vm.test;
        }

        public override void Execute(object parameter)
        {
            _vm.PairFound = false;
            RightPair();
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
            int ID;
            if (_parent.CurrentQuestionBank == "Default")
            {
                ID = 0;
            }
            else
            {
                ID = _parent.UserID;
            }
            found = false;
            string textblock2Contains = _vm.TextBlock2Contains;
            List<string> CheckWithTextBlock = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question LIKE '{_vm.TextBlockContains}' AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = { ID }", 1);
            string textblockContains = _vm.TextBlockContains;
            List<string> CheckWithTextBlock2 = _parent.Database.ReadData("QuestionBanks", "Answer", $"Question LIKE '{_vm.TextBlock2Contains}' AND BankName = '{_parent.CurrentQuestionBank}' AND UserID = {ID}", 1);

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
