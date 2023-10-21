using MySql.Data.MySqlClient;
using System.Data;
using static BCrypt.Net.BCrypt;
using static SEI.Classes.ConnectSEI;

namespace SEI.Classes
{
    public class Auth
    {
        public bool AutenticarAluno(string nomeUsuario, string senha)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "RecuperarCredAluno"
            };

            command.Parameters.AddWithValue("@p_Login_Name_ALuno", nomeUsuario);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string salt = reader.GetString(0);

                bool senhacorreta = Verify(senha, salt);
                command.Connection.Close();
                return senhacorreta;
            }
            else
            {
                Console.WriteLine("Credenciais Inválidas, Login Incorreto");
                command.Connection.Close();
                return false;
            }
        }

        public bool AutenticarResponsavel(string nomeUsuario, string senha)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "RecuperarCredResponsavel"
            };

            command.Parameters.AddWithValue("@p_Login_Name_Responsavel", nomeUsuario);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string salt = reader.GetString(0);

                bool senhacorreta = Verify(senha, salt);
                command.Connection.Close();
                return senhacorreta;
            }
            else
            {
                Console.WriteLine("Credenciais Inválidas, Login Incorreto");
                command.Connection.Close();
                return false;
            }
        }

        public bool AutenticarProfessor(string nomeUsuario, string senha)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "RecuperarCredProf"
            };

            command.Parameters.AddWithValue("@p_Login_Name_Professor", nomeUsuario);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string salt = reader.GetString(0);

                bool senhacorreta = Verify(senha, salt);
                command.Connection.Close();
                return senhacorreta;
            }
            else
            {
                Console.WriteLine("Credenciais Inválidas, Login Incorreto");
                command.Connection.Close();
                return false;
            }
        }
        
        public bool AutenticarSecretario(string nomeUsuario, string senha)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "RecuperarCredSecretario"
            };

            command.Parameters.AddWithValue("@p_Login_Name_Secretario", nomeUsuario);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string salt = reader.GetString(0);

                bool senhacorreta = Verify(senha, salt);
                command.Connection.Close();
                return senhacorreta;
            }
            else
            {
                Console.WriteLine("Credenciais Inválidas, Login Incorreto");
                command.Connection.Close();
                return false;
            }
        }
    }
}
