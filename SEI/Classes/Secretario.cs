using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using static SEI.Classes.ConnectSEI;
using static SEI.Classes.PasswordCrypt;

namespace SEI.Classes
{
    public class Secretario : Pessoa
    {
        public string CPFSecretario { get; set; } = "";
        public string CargoSecretario { get; set; } = "";
        public string TelSecretario { get; set; } = "";
        public int IdSecretario { get; set; }

        public void CadastrarSecretario(Secretario secretario)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "CadastrarSecretario"
            };

            var encryptedPassword = EncryptPass(secretario.Password);

            Dictionary<string, object> parametros = new()
            {
                {"@p_Nome_Secretario", secretario.Nome},
                {"@p_Cargo_Secretario", secretario.CargoSecretario},
                {"@p_CPF_Secretario", secretario.CPFSecretario},
                {"@p_Registro_Geral_Secretario", secretario.RegistroGeral},
                {"@p_Telefone_Secretario", secretario.TelSecretario},
                {"@p_Login_Name_Secretario", secretario.Login},
                {"@p_Senha_Secretario", encryptedPassword.PasswordHash},
                {"@p_Salt_Secretario", encryptedPassword.PasswordSalt}
            };
            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
            command.ExecuteNonQuery();
            command.Connection.Close();
            Console.WriteLine("secretario cadastrado");
        }
        public Secretario? BuscarSecretario(int id)
        {
            try
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BuscarSecretario"
                };
                IdSecretario = id;
                command.Parameters.AddWithValue("@p_ID_Secretario", IdSecretario);

                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Secretario secretario = new()
                    {
                        Nome = reader.GetString(0),
                        Sobrenome = reader.GetString(1),
                        CargoSecretario = reader.GetString(2),
                        CPFSecretario = reader.GetString(3),
                        RegistroGeral = reader.GetString(4),
                        TelSecretario = reader.GetString(5),
                        Login = reader.GetString(6)
                    };
                    command.Connection.Close();
                    return secretario;
                }
                else
                {
                    Console.WriteLine("Secretario não encontrado");
                    command.Connection.Close();
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void EditarSecretatio(
            int ID,
            string nome,
            string sobre,
            string cargo,
            string cpf,
            string registro,
            string telefone,
            string login
            )
        {
            IdSecretario = ID;
            Nome = nome;
            Sobrenome = sobre;
            CargoSecretario = cargo;
            CPFSecretario = cpf;
            Login = login;
            RegistroGeral = registro;
            TelSecretario = telefone;

            Dictionary<string, object> parametros = new()
            {
                { "@p_ID_Secretario", IdSecretario},
                { "@p_Nome_Secretario", Nome },
                { "@p_Cargo_Secretario", CargoSecretario },
                { "@p_CPF_Secretario", CPFSecretario },
                { "@p_Registro_Geral_Secretario", RegistroGeral },
                { "@p_Telefone_Secretario", TelSecretario },
                { "@p_Login_Name_Secretario", Login }
            };

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AtualizarSecretario"
            };

            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            command.ExecuteNonQuery();
            command.Connection.Close();
        }
        public void ExcluirSecretario(int ID)
        {
            IdSecretario = ID;
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "ExcluirSecretario"
            };

            command.Parameters.AddWithValue("@p_ID_Secretario", IdSecretario);
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
    }
}
