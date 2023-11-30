using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SEI.Classes
{
    public static class ConnectSEI
    {
        private static readonly string _Server = "104.154.22.111";
        private static readonly string _UserID = "root";
        private static readonly string _Password = "tccunip";
        private static readonly string _Database = "sei-1";

        public static string Server => _Server;
        public static string UserID => _UserID;
        public static string Password => _Password;
        public static string Database => _Database;


        public static MySqlConnection ConnectToDB()
        {
            try
            {
                using MySqlConnection con = new(@"server=104.154.22.111;userid=root;password=tccunip;database=SEI_DB");
                con.Open();

                Console.WriteLine("sql connectado");

                return con;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
