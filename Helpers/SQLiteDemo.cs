using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Based on: https://www.codeguru.com/dotnet/using-sqlite-in-a-c-application/
namespace SQLDatabase
{
    
    public class Database
    {
        //Intitialise
        private SQLiteConnection Connection { get; set; }
        //Constructor
        public Database()
        {
            Connection = CreateConnection();;

        }

        private SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Creates the connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; " +
                "Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        public void Execute(string query)
        {
            //All the methods that do not return anything, such as create table, are all executed here. 
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();


        }

        //The methods that don't return anything all have the same format:
        //Create SQL Statement by concatenating 
        //Call Execute Method
        public void CreateTable(string tablename, string tableRequirements)
        {   
            string Createsql = $"CREATE TABLE IF NOT EXISTS {tablename}({tableRequirements});"; 
            Execute(Createsql);
        }

        public void InsertData(string tablename, string columns, string values)
        {
            string Insertsql = $"INSERT INTO {tablename}({columns}) VALUES({values}); ";
            Execute(Insertsql);
        }

        public void UpdateData(string tablename, string set, string condition)
        {
            string Updatesql = $"UPDATE {tablename} SET {set} WHERE {condition};";
            Execute(Updatesql);

        }
        public void DeleteData(string tablename, string condition)
        {
            string Deletesql = $"DELETE FROM {tablename} WHERE {condition};";
            Execute(Deletesql);
        }

        public void Droptable(string tablename)
        {
            string Dropsql = $"DROP TABLE {tablename};";
            Execute(Dropsql);
        }

        public List<string> ReadData(string tablename, string Column, string condition, int NoColums)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            //myreader - Holds any data that is returned select
            List<string> myreader = new List<string>();
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT {Column} FROM {tablename} WHERE {condition};";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                // The for loop makes sure that multiple fields from the same record can be returned 
                for (int i = 0; i < NoColums; i++)
                {
                    //as not all of the fields are strings, the catch handles any of the integer fields
                    try
                    {
                        //adds i (field) to myreader 
                        myreader.Add(sqlite_datareader.GetString(i));
                    }
                    catch (Exception)
                    {
                        int temp = (sqlite_datareader.GetInt32(i));
                        myreader.Add(temp.ToString());
                        
                    }
                }



            }
            
            return myreader;
        }

        
        public int GetSize(string tablename, string ID, string Condition)
        {
            //This gets the size of a database and returns the value
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            int Count = 0;
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT COUNT({ID}) FROM {tablename} {Condition};";
            Count = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            return Count;
        }
    }
}
