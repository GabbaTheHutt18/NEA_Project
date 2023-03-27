using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDatabase
{
    public class Database
    {
        private SQLiteConnection Connection { get; set; }

        public Database(string tablename, string tableRequirements)
        {
            Connection = CreateConnection();
            CreateTable(tablename, tableRequirements);

        }

        private SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
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
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();


        }

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

        public void UpdateData(string tablename, string set)
        {
            string Updatesql = $"UPDATE {tablename} SET {set};";
            Execute(Updatesql);
        }

        public string ReadData(string tablename,string Column, string condition)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            string myreader = "";
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT {Column} FROM {tablename} WHERE {condition}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                myreader = sqlite_datareader.GetString(0);
               // Console.WriteLine(myreader);
            }
            //Connection.Close();
            return myreader;
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

        public int GetSize(string tablename, string ID)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            string myreader = "";
            int Count = 0;
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT COUNT({ID}) FROM {tablename}";
            Count = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            //Connection.Close();


            return Count;
        }
    }
}
