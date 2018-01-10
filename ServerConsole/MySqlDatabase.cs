using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ServerConsole
{
    class MySqlDatabase
    {
        public MySqlConnection connection;

        string connectionString = "SERVER=localhost;DATABASE=mysql;UID=root;PASSWORD=;";

        public bool OpenConnection()
        {
            connection = new MySqlConnection(connectionString);

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
                }
                return false;
            }
        }
        public bool CloseConnection()
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
        public bool insertToDB(string Query)
        {
            string query = Query;
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command

                    //dodac try cach dla duplikatow i bledow
                    cmd.ExecuteNonQuery();

                    this.CloseConnection();

                    return true;
                }
                catch (Exception exc)
                {

                    return false;
                }


                //close connection


            }
            else
            {
                return false;
            }
        }

        public DataSet getDataSet(string Command)
        {
            string sqlCmd = Command;
            DataSet DS = new DataSet();
            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);
                cmd.CommandType = CommandType.Text;
                // MySqlDataReader rdr = cmd.ExecuteReader();

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlCmd, connection);

                mySqlDataAdapter.Fill(DS);
                mySqlDataAdapter.Dispose();

            }
            return DS;

        }
        public int checkLogin(string Szukana)
        {
            string szukana = Szukana;
            int Count = -1;
            string sqlCmd = "select count(*) from uzytkownicy where login = '" + szukana + "';";

            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(sqlCmd, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }

        }
    }
}
