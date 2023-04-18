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
        //initialise
        MainWindowViewModel _parent;
        private string _BankName = "Default";
        private string _question = String.Empty;
        private string _answer = String.Empty;

        public ICommand MenuButtonClickedCommand { get; }
        public ICommand AddToQuestionBankCommand { get; }
        
        //constructor
        public QuestionBankCreateViewModel(MainWindowViewModel Parent) 
        {
            _parent = Parent;
            MenuButtonClickedCommand = new SimpleCommand(_ => MenuButtonClicked());
            AddToQuestionBankCommand = new SimpleCommand(_ => AddQuestionsAndAnswers());
        }

        public string BankName { get => _BankName; set { _BankName = value; } }
        public string Question { get => _question; set { _question = value; } }
        public string Answer { get => _answer; set { _answer = value; } }

        //adds questions and answers to the question bank database, as long as all conditions are met (answer and question can't be null
        //and bank name can't be null or "Default"). Then a unique QuestionID is calculated before the question and answer are added.
        public void AddQuestionsAndAnswers()
        {
            int QuestionID = 0;
            if (BankName == String.Empty || BankName == "Default")
            {
                MessageBox.Show("Please Enter a suitable Question Bank Name! (Default is already in use)");

            }
            else
            { 
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
                        _parent.Database.InsertData($"QuestionBanks", "UserID, BankName, QuestionID,Question, Answer", $"{_parent.UserID},'{BankName}',{QuestionID},'{Question}', '{Answer}'"); //missing ''
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        //calculates the number of questions in a given question bank (to help calculate the Question ID)
        public int NumberOfQuestions()
        {
            return _parent.Database.GetSize("QuestionBanks", "QuestionID", $"WHERE UserID = {_parent.UserID} AND BankName = '{BankName}'");
        }
        //when the button is pressed, the method in MainWindowViewModel is called to change the page. 
        private void MenuButtonClicked()
        {
            _parent.ChangeToQuestionBankMenuPage();
        }
    }
}
