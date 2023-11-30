using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using static SEI.Classes.ConnectSEI;
using static SEI.Classes.PasswordCrypt;

namespace SEI.Classes
{
    public class Aluno : Pessoa
    {
        public string RelacaoResponsavel { get; set; } = "relacao";
        public string Matricula { get; set; } = "";
        public int IdResponsavel { get; set; } = 0;

        public void CadastrarAluno(
            string nome,
            string sobre,
            string login,
            string gender,
            DateOnly datanasc,
            string ender,
            string relacao,
            int ID,
            string registro,
            string email,
            string senha)
        {
            Nome = nome;
            Sobrenome = sobre;
            Login = login;
            Gender = gender;
            DataNasc = datanasc;
            Endereco = ender;
            RelacaoResponsavel = relacao;
            IdResponsavel = ID;
            RegistroGeral = registro;
            Email = email;
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
                    CommandText = "CadastrarAluno"
                };

                //Chama o método que criptografa a senha
                var encryptedPassword = EncryptPass(Password);

                //Dicionario que adiciona os parametro para cadastro do Aluno
                Dictionary<string, object> parametros = new()
                {
                    { "@p_Nome_Aluno", Nome },
                    { "@p_Sobrenome_Aluno", Sobrenome },
                    { "@p_Login_Name_ALuno", Login },
                    { "@p_Genero_Aluno", Gender },
                    { "@p_Data_Nascimento_Aluno", DataNasc },
                    { "@p_Endereco", Endereco },
                    { "@p_Relacao_Responsavel", RelacaoResponsavel },
                    { "@p_ID_Responsavel", IdResponsavel },
                    { "@p_Registro_Geral_Aluno", RegistroGeral },
                    { "@p_Email_Aluno", Email },
                    { "@p_Senha_Aluno", encryptedPassword.PasswordHash },
                    { "@p_Salt_Aluno", encryptedPassword.PasswordSalt }
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
            Console.WriteLine("Aluno Cadastrado");
        }

        public Aluno? BuscarAluno(string mat)
        {
            try
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "BuscarAluno"
                };
                Matricula = mat;
                command.Parameters.AddWithValue("@p_Matricula", Matricula);

                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Aluno aluno = new()
                    {
                        Matricula = reader.GetString(0),
                        Nome = reader.GetString(1),
                        Sobrenome = reader.GetString(2),
                        Login = reader.GetString(3),
                        Gender = reader.GetString(4),
                        DataNasc = DateOnly.FromDateTime(reader.GetDateTime(5)),
                        Endereco = reader.GetString(6),
                        RelacaoResponsavel = reader.GetString(7),
                        IdResponsavel = reader.GetInt32(8),
                        RegistroGeral = reader.GetString(9),
                        Email = reader.GetString(10)
                    };
                    command.Connection.Close();
                    return aluno;
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
                throw;
            }
        }

        public void EditarAluno(
            string mat,
            string nome,
            string sobre,
            string login,
            string gender,
            DateOnly datanasc,
            string ender,
            string relacao,
            int ID,
            string registro,
            string email)
        {
            Matricula = mat;
            Nome = nome;
            Sobrenome = sobre;
            Login = login;
            Gender = gender;
            DataNasc = datanasc;
            Endereco = ender;
            RelacaoResponsavel = relacao;
            IdResponsavel = ID;
            RegistroGeral = registro;
            Email = email;

            Dictionary<string, object> parametros = new()
            {
                { "@p_Matricula", Matricula },
                { "@p_Nome_Aluno", Nome },
                { "@p_Sobrenome_Aluno", Sobrenome },
                { "@p_Login_Name_ALuno", Login },
                { "@p_Genero_Aluno", Gender },
                { "@p_Data_Nascimento_Aluno", DataNasc },
                { "@p_Endereco", Endereco },
                { "@p_Relacao_Responsavel", RelacaoResponsavel },
                { "@p_ID_Responsavel", IdResponsavel },
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

        public void DeletarAluno(string mat)
        {
            Matricula = mat;

            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "ExcluirAluno"
            };
            command.Parameters.AddWithValue ("@p_Matricula", mat);
            command.ExecuteNonQuery();
        }
    }
}
