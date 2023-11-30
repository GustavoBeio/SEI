using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using static SEI.Classes.ConnectSEI;
using static SEI.Classes.PasswordCrypt;

namespace SEI.Classes
{
    public class Professor : Pessoa
    {
        public string Especialidade { get; set; } = "";
        public string Telefone { get; set; } = "";
        public int Id { get; set; } = 0;

        public void CadastrarProfessor(
            string nome,
            string especialidade,
            string telefone,
            string email,
            string login,
            string senha)
        {
            Nome = nome;
            Especialidade = especialidade;
            Telefone = telefone;
            Email = email;
            Login = login;
            Password = senha;
            try
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CadastrarProfessor"
                };

                //Chama o método que criptografa a senha
                var encryptedPassword = EncryptPass(Password);

                //Dicionario que adiciona os parametro para cadastro do Aluno
                Dictionary<string, object> parametros = new()
                {
                    {"@p_Nome_Professor", Nome },
                    { "@p_Especialidade_Professor", Especialidade},
                    {"@p_Telefone_Professor", Telefone},
                    {"@p_Email_Professor", Email},
                    {"@p_Login_Name_Professor", Login},
                    {"@p_Senha_Professor", encryptedPassword.PasswordHash },
                    {"@p_Salt_Professor", encryptedPassword.PasswordSalt }
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
        }
        public Professor? BuscarProfessor(int buscarProfessorId)
        {
            try
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BuscarProfessor"
                };
                Id = buscarProfessorId;
                command.Parameters.AddWithValue("@p_ID_Professor", Id);

                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Professor professor = new()
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Especialidade = reader.GetString(2),
                        Telefone = reader.GetString(3),
                        Email = reader.GetString(4),
                        Login = reader.GetString(5)
                    };
                    command.Connection.Close();
                    return professor;
                }
                else
                {
                    Console.WriteLine("Professor não encontrado");
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

        public void EditarProfessor(
            int professorId,
            string nome,
            string especialidade,
            string telefone,
            string email)
        {
            Id = professorId;
            Nome = nome;
            Especialidade = especialidade;
            Telefone = telefone;
            Email = email;

            Dictionary<string, object> parametros = new()
            {
                { "@p_ID_Professor", Id },
                { "@p_Nome_Professor", Nome },
                { "@p_Especialidade_Professor", Especialidade },
                { "@p_Telefone_Professor", Telefone },
                { "´@p_Email_Professor", Email }
            };

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AtualizarProfessor"
            };

            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void DeletarProfessor(int professorId)
        {
            Id = professorId;

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "ExcluirProfessor"
            };
            command.Parameters.AddWithValue("@p_Matricula", Id);
            command.ExecuteNonQuery();
        }
    }
}
