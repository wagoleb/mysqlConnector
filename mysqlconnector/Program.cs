using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace mysqlconnector
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect(string host, string database, string user, string password)
        {
            this.server = host;
            this.database = database;
            this.uid = user;
            this.password = password;

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                    case 1042:
                        Console.WriteLine("Problem: {0}", ex.Message);
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        // Select statement
        public List<String> Select(string query)
        {
            if (this.OpenConnection() == true)
            {
                List<String> result = new List<String>();
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    result.Add(dataReader["Tables_in_world"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                return result;
            }
            else
            {
                return null;
            }
        }

            /*
        Count statement
        public int Count()
        {
        } */

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DBConnect polaczenie = new DBConnect("localhost", "world", "bonowg", "P@$sw0rd001");
            foreach (var item in polaczenie.Select("SHOW tables"))
            {
                Console.WriteLine(item);
            }
        }
    }
}
