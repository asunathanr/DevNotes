using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SqlLiteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = CreateConnection();
            CreateTable(conn);
            InsertData(conn);
            ReadData(conn);
            conn.Close();
            Console.Read();
        }

        static SQLiteConnection CreateConnection()
        {
            const string CONNECTION_ARGUMENTS = "Data Source = database.db;Version=3;New=True;Compress=True";
            var conn = new SQLiteConnection(CONNECTION_ARGUMENTS);
            RemoveDatabase();
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                // Unable to create database
            }
            return conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {
            const string TABLE_ARGUMENTS = "CREATE TABLE SampleTable" +
                "(Col1 VARCHAR(20), Col2 INT)";
            var command = conn.CreateCommand();
            command.CommandText = TABLE_ARGUMENTS;
            command.ExecuteNonQuery();
        }

        static void InsertData(SQLiteConnection conn)
        {
            const string DATA_ARGUMENTS = "INSERT INTO SampleTable (Col1, Col2) VALUES ('Test text ', 1)";
            var command = conn.CreateCommand();
            command.CommandText = DATA_ARGUMENTS;
            command.ExecuteNonQuery();
        }

        static void ReadData(SQLiteConnection conn)
        {
            const string READ_ARGUMENTS = "select * from SampleTable";
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = READ_ARGUMENTS;
            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine(dataReader.GetString(0));
            }
        }

        static void RemoveDatabase()
        {
            const string DB_PATH = "C:\\Users\\absna\\source\\repos\\DevNotes\\SqlLiteTest\\bin\\Debug\\database.db";
            if (System.IO.File.Exists(DB_PATH))
            {
                System.IO.File.Delete(DB_PATH);
            }
        }
    }
}
