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
        public string CPFSecretario { get; set; } = "00011122233";
        public string CargoSecretario { get; set; } = "cargo";
        public string TelSecretario { get; set; } = "telefone";
        public int IdSecretario { get; set; }

        public void CadastrarSecretario()
        {
            Console.WriteLine("professor cadastrado");
        }
        public Secretario? BuscarSecretario(string CPF)
        {
            try
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BuscarSecretario"
                };
                CPFSecretario = CPF;
                command.Parameters.AddWithValue("@p_CPF_Secretario", CPFSecretario);

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
                    Console.WriteLine("Aluno não encontrado");
                    command.Connection.Close();
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                throw ex;
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
                { "@p_Matricula", IdSecretario},
                { "@p_Nome_Aluno", Nome },
                { "@p_Sobrenome_Aluno", Sobrenome },
                { "@p_Login_Name_ALuno", Login },
                { "@p_Genero_Aluno", Gender },
                { "@p_Data_Nascimento_Aluno", DataNasc },
                { "@p_Endereco", Endereco },
                { "@p_Registro_Geral_Aluno", RegistroGeral },
                { "@p_Email_Aluno", Email }
            };

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AtualizarAluno"
            };

            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            command.ExecuteNonQuery();
            command.Connection.Close();
        }
        public void ExcluirSecretario(int id)
        {
            IdSecretario = id;

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "ExcluirSecretario"
            };
        }
    }
}
