using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using static SEI.Classes.ConnectSEI;

namespace SEI.Classes
{
    public class Turma
    {
        public int IdTurma { get; set; }
        public string NomeTurma { get; set; } = "";
        public DateOnly AnoLetivo { get; set; }
        public int ProfessorOrientador { get; set; }

        public void CadastrarTurma(Turma turma)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "CadastrarTurma"
            };
            Dictionary<string, object> parametros = new()
            {
                { "@p_ID_Turma", turma.IdTurma },
                { "@p_Nome_Turma", turma.NomeTurma },
                { "@p_Ano_Letivo", turma.AnoLetivo },
                { "@p_Professor_Orientador", turma.ProfessorOrientador }
            };
            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
        public void Excluirturma(int id)
        {
            IdTurma = id;
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "ExcluirTurma"
            };
            command.Parameters.AddWithValue("@p_ID_Turma", IdTurma);
            command.ExecuteNonQuery();
        }
        public Turma? Buscarturma(int id)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "BuscarTurma"
            };

            IdTurma = id;
            command.Parameters.AddWithValue("@p_ID_Turma", IdTurma);

            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Turma turma = new()
                {
                    NomeTurma = reader.GetString(1),
                    AnoLetivo = DateOnly.FromDateTime(reader.GetDateTime(2)),
                    ProfessorOrientador = reader.GetInt32(3)
                };
                command.Connection.Close();
                return turma;
            }
            else
            {
                Console.WriteLine("Turma não encontrada");
                command.Connection.Close();
                return null;
            }
        }

        public void EditarTurma(Turma turma)
        {
            Dictionary<string, object> parametros = new()
            {
                { "@p_ID_Turma", turma.IdTurma },
                { "@p_Nome_Turma", turma.NomeTurma },
                { "@p_Ano_Letivo", turma.AnoLetivo },
                { "@p_Professor_Responsavel", turma.ProfessorOrientador }
            };
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AtualizarTurma"
            };
            foreach(var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void AdicionarAluno(string alunoid, int turmaid)
        {
            if(Buscarturma(turmaid) != null)
            {
                MySqlCommand command = new()
                {
                    Connection = ConnectToDB(),
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AdicionarAluno"
                };
                Dictionary<string, object> parametros = new()
                {
                    { "@p_Matricula", alunoid },
                    { "@p_ID_Turma", turmaid }
                };
                foreach (var parametro in parametros)
                {
                    command.Parameters.AddWithValue(parametro.Key, parametro.Value);
                }
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            else
            {
                
            }
        }
    }
}
