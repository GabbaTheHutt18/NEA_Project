using NEA_Project.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NEA_Project.ViewModels
{
    public class QuestionBankCreatePageViewModel : ObservableObject
    {

        private MainWindowViewModel _parent;
        private string _FileName = "Default";
        private string _question = String.Empty;
        private string _answer = String.Empty;

        public ICommand CreateQuestionBankCommand { get; }
        public ICommand AddToQuestionBankCommand { get; }
        public QuestionBankCreatePageViewModel(MainWindowViewModel parent)
        {
            _parent = parent;
            CreateQuestionBankCommand = new SimpleCommand(_ => CreateQuestionBank());
            AddToQuestionBankCommand = new SimpleCommand(_ => AddQuestionsAndAnswers());

        }

        public string FileName { get => _FileName; set { _FileName = value; } }
        public string Question { get => _question; set { _question = value; } }
        public string Answer { get => _answer; set { _answer = value; } }


        public void CreateQuestionBank()
        {
            //_parent.Database.CreateTable($"{FileName}", "Question VARCHAR(100), Answer VARCHAR(150)");
            //Database.CreateTable("QuestionBanks", "UserID INT, BankName VARCHAR(100), Question VARCHAR(100), Answer VARCHAR(150)");

        }
        public void AddQuestionsAndAnswers()
        {
            int QuestionID = 0;
            if (FileName == String.Empty)
            {
                MessageBox.Show("No bank exists, make sure you press create first!");

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

            //
            return _parent.Database.GetSize("QuestionBanks", "QuestionID", $"WHERE UserID = {_parent.UserID} AND BankName = '{FileName}'");
        }
    }
}
