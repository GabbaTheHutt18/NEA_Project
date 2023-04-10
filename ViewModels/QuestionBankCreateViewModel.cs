using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Project.ViewModels
{
    public class QuestionBankCreateViewModel
    {
        MainWindowViewModel _parent;
        private string _FileName = "Default";
        private string _question = String.Empty;
        private string _answer = String.Empty;

        public ICommand MenuButtonClickedCommand { get; }
        public ICommand AddToQuestionBankCommand { get; }
       
        public QuestionBankCreateViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            MenuButtonClickedCommand = new SimpleCommand(_ => MenuButtonClicked());
            AddToQuestionBankCommand = new SimpleCommand(_ => AddQuestionsAndAnswers());
        }

        public string FileName { get => _FileName; set { _FileName = value; } }
        public string Question { get => _question; set { _question = value; } }
        public string Answer { get => _answer; set { _answer = value; } }

        public void AddQuestionsAndAnswers()
        {
            int QuestionID = 0;
            if (FileName == String.Empty || FileName == "Default")
            {
                MessageBox.Show("Please Enter a suitable Question Bank Name! (Default is already in use)");

            }
            else
            { //Needs to be in an else NOT nested :D
                if (Answer == String.Empty || Question == String.Empty)
                {
                    MessageBox.Show("Oh no! Please enter both your Question and answer!");
                }
                else
                {
                    try
                    {
                        QuestionID = NumberOfQuestions();
                    }
                    catch (Exception)
                    {


                    }

                    try
                    {
                        QuestionID += 1;
                        _parent.Database.InsertData($"QuestionBanks", "UserID, BankName, QuestionID,Question, Answer", $"{_parent.UserID},'{FileName}',{QuestionID},'{Question}', '{Answer}'"); //missing ''
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        public int NumberOfQuestions()
        {
            return _parent.Database.GetSize("QuestionBanks", "QuestionID", $"WHERE UserID = {_parent.UserID} AND BankName = '{FileName}'");
        }

        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
    }
}
