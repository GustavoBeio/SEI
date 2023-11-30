using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using static SEI.Classes.ConnectSEI;
using static SEI.Classes.PasswordCrypt;

namespace SEI.Classes
{
    public class Responsavel : Pessoa
    {
        public int ID { get; set; } = 0;
        public string Telefone { get; set; } = "";
        public void CadastrarResponsavel(
            int id,
            string nome,
            string sobre,
            string telefone,
            string login,
            string senha)
        {
            ID = id;
            Nome = nome;
            Sobrenome = sobre;
            Telefone = telefone;
            Login = login;
            Password = senha;

            try
            {
                /*
                 * Cria-se uma nova conexão com o banco e prepara a procedure para cadastrar o aluno.
                 * 
                 * Verifica se o Responsável do Aluno já possúi cadastro, caso não possua, retorna uma exceção
                */
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CadastrarResponsavel"
                };

                //Chama o método que criptografa a senha
                var encryptedPassword = EncryptPass(Password);

                //Dicionario que adiciona os parametro para cadastro do Aluno
                Dictionary<string, object> parametros = new()
                {
                    { "@p_Nome_Responsavel", Nome },
                    { "@p_Sobrenome_Responsavel", Sobrenome },
                    { "@p_Telefone_Responsavel", Telefone },
                    { "@p_Login_Name_Responsavel", Login },
                    { "@p_Senha_Responsavel", encryptedPassword.PasswordHash },
                    { "@p_Salt_Responsavel", encryptedPassword.PasswordSalt }
                };
                //Itera pelo dicionario adicionando os a StoredProcedure
                foreach (var parametro in parametros)
                {
                    command.Parameters.AddWithValue(parametro.Key, parametro.Value);
                }
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Responsavel Cadastrado");
        }

        public Responsavel? BuscarResponsavel (int id)
        {
            try
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BuscarAluno"
                };
                ID = id;
                command.Parameters.AddWithValue("@p_ID_Responsavel", ID);

                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Responsavel responsavel = new()
                    {
                        Nome = reader.GetString(1),
                        Sobrenome = reader.GetString(2),
                        Telefone = reader.GetString(3),
                        Login = reader.GetString(4)
                    };
                    command.Connection.Close();
                    return responsavel;
                }
                else
                {
                    Console.WriteLine("Responsavel não encontrado");
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

        public void EditarResponsavel(
            int id,
            string nome,
            string sobre,
            string telefone,
            string login)
        {
            ID = id;
            Nome = nome;
            Sobrenome = sobre;
            Telefone = telefone;
            Login = login;

            Dictionary<string, object> parametros = new()
            {
                { "@p_ID_Responsavel", ID },
                { "@p_Nome_Responsavel", Nome },
                { "@p_Sobrenome_Responsavel", Sobrenome },
                { "@p_Telefone_Responsavel", Telefone },
                { "@p_Login_Name_Responsavel", Login }
            };
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AtualizarResponsavel"
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
            ID = id;

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "ExcluirSecretario"
            };
            command.Parameters.AddWithValue("@p_Matricula", ID);
            command.ExecuteNonQuery();
        }
    }
}
